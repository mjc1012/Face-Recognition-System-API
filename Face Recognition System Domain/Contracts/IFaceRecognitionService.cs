
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using OpenCvSharp;
using System.Drawing;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IFaceRecognitionService
    {

        Mat DetectFace(Bitmap img);
        int RecognizeFace(FaceToRecognizeDto face);

        void TrainModel();
    }
}
