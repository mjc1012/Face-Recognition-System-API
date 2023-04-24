
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;
using System.Drawing;
using System.IO.Compression;
using Microsoft.ML;
using Tensorflow;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Vision;
using System.Runtime.CompilerServices;
using AForge.Imaging.Filters;
using Face_Recognition_System_Domain.Contracts;
using static Face_Recognition_System_Data.Constants;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Domain.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly MLContext _mlContext;
        public FaceRecognitionService()
        {
            _mlContext = new();
        }

        public Mat DetectFace(Bitmap img)
        {
            try
            {
                Net faceNet = CvDnn.ReadNetFromCaffe(PathConstants.FaceDetectionPrototxtPath, PathConstants.FaceDetectionCaffeModelPath);

                Mat newImage = img.ToMat();
                Cv2.CvtColor(newImage, newImage, ColorConversionCodes.RGBA2RGB);
                int frameHeight = newImage.Rows;
                int frameWidth = newImage.Cols;

                Mat blob = CvDnn.BlobFromImage(newImage, 1.0, new Size(224, 224),
                    new Scalar(104, 117, 123), false, false);

                faceNet.SetInput(blob, "data");

                Mat detection = faceNet.Forward("detection_out");
                Mat detectionMat = new(detection.Size(2), detection.Size(3), MatType.CV_32F,
                    detection.Ptr(0));

                float confidence = detectionMat.At<float>(0, 2);
                int x1 = (int)(detectionMat.At<float>(0, 3) * frameWidth);
                int y1 = (int)(detectionMat.At<float>(0, 4) * frameHeight);
                int x2 = (int)(detectionMat.At<float>(0, 5) * frameWidth);
                int y2 = (int)(detectionMat.At<float>(0, 6) * frameHeight);

                Rect roi = new(x1, y1, x2 - x1, y2 - y1);
                return newImage.Clone(roi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Bitmap ToGrayscale(Bitmap image)
        {
            try
            {

                Grayscale filter = new(0.2125, 0.7154, 0.0721);
                return filter.Apply(image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Bitmap SharpenImage(Bitmap image)
        {
            try
            {
                GaussianSharpen filter = new(4, 11);
                return filter.Apply(image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Bitmap HistogramEqualizationGrayscale(Mat image)
        {
            try
            {
                Cv2.EqualizeHist(image, image);
                return image.ToBitmap();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RecognizeFace(FaceToRecognizeDto face)
        {
            try
            {
                ITransformer mlModel = _mlContext.Model.Load(PathConstants.FaceRecognitionModelPath, out var _);
                Lazy<PredictionEngine<ModelInput, ModelOutput>> _PredictEngine = new(() => _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel), true);

                string path = Path.Combine(PathConstants.FacesToRecognizePath, face.ImageFile);

                Bitmap image = new(path);
                image = SharpenImage(image);
                image = ToGrayscale(image);
                image = HistogramEqualizationGrayscale(image.ToMat());

                ModelInput faceData = new()
                {
                    Image = image.ToMat().ToBytes(),
                };

                ModelOutput prediction = _PredictEngine.Value.Predict(faceData);

                if(prediction.Score.Max() >= 0.9)
                {
                    return prediction.PredictedLabel;
                }
                return -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void TrainModel()
        {
            try
            {
                IEnumerable<ImageData> images = LoadImagesFromDirectory(PathConstants.AugmentedFacesPath);

                IDataView imageData = _mlContext.Data.LoadFromEnumerable(images);

                IDataView shuffledData = _mlContext.Data.ShuffleRows(imageData);

                EstimatorChain<ImageLoadingTransformer> preprocessingPipeline = _mlContext.Transforms.Conversion.MapValueToKey(
                                                                            inputColumnName: "Label",
                                                                            outputColumnName: "LabelAsKey")
                                            .Append(_mlContext.Transforms.LoadRawImageBytes(
                                                                            outputColumnName: "Image",
                                                                            imageFolder: PathConstants.AugmentedFacesPath,
                                                                            inputColumnName: "ImagePath"));

                IDataView preProcessedData = preprocessingPipeline.Fit(shuffledData).Transform(shuffledData);

                TrainTestData trainSplit = _mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.1);

                IDataView trainSet = trainSplit.TrainSet;
                IDataView validationSet = trainSplit.TestSet;

                ITransformer trainedModel = TrainingPipeline(_mlContext, validationSet).Fit(trainSet);

                _mlContext.Model.Save(trainedModel, trainSet.Schema, PathConstants.FaceRecognitionModelPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static EstimatorChain<KeyToValueMappingTransformer> TrainingPipeline(MLContext mlContext, IDataView validationSet)
        {
            try
            {
                return mlContext.MulticlassClassification.Trainers.ImageClassification(
                new ImageClassificationTrainer.Options()
                {
                    FeatureColumnName = "Image",
                    LabelColumnName = "LabelAsKey",
                    ValidationSet = validationSet,
                    Arch = ImageClassificationTrainer.Architecture.MobilenetV2,
                    Epoch = 1000000,
                    BatchSize = 32,
                    EarlyStoppingCriteria = new ImageClassificationTrainer.EarlyStopping(),
                    LearningRateScheduler = new Microsoft.ML.Trainers.LsrDecay(),
                    MetricsCallback = (metrics) => Console.WriteLine(metrics),
                })
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder)
        {
            var files = Directory.GetFiles(folder, "*",
                searchOption: SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png") && (Path.GetExtension(file) != ".jpeg"))
                    continue;

                var label = Int32.Parse(Directory.GetParent(file).Name);

                yield return new ImageData()
                {
                    ImagePath = file,
                    Label = label
                };
            }
        }
    }
}
