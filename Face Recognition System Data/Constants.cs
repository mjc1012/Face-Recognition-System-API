using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Data
{
    public class Constants
    {
        public class PathConstants
        {
            public const string AugmentedFacesPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Data\\Augmented Faces";
            public const string FacesToRecognizePath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Data\\Faces To Recognize";
            public const string FaceDatasetPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Data\\Face Dataset";
            public const string FaceDetectionPrototxtPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Domain\\Face Detection Model\\deploy.prototxt.txt";
            public const string FaceDetectionCaffeModelPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Domain\\Face Detection Model\\res10_300x300_ssd_iter_140000_fp16.caffemodel";
            public const string FaceRecognitionModelPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Domain\\Face Recognition Model\\FaceRecognitionModel.zip";
            public const string FaceRecognitionFolderPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Domain\\Face Recognition Model";
            public const string FaceExpressionPath = "D:\\THESIS FINAL OUTPUT\\face recognition backend\\Face Recognition System Server\\Face Recognition System Data\\Face Expression Samples";
        }

        public class BaseConstants
        {
            public const string RetrievedData = "Retrieved Data";
            public const string ErrorList = "Error List";
        }

        public class AugmentedFaceConstants
        {
            public const string EntryInvalid = "Augmented face entry is not valid.";
            public const string SuccessAdd = "Augmented face added successfully.";
        }

        public class FaceRecognitionStatusConstants
        {
            public const string EntryInvalid = "Face recognition status entry is not valid.";
            public const string SuccessAdd = "Face recognition status added successfully.";
            public const string DoesNotExist = "Face recognition status does not exist.";
            public const string Exists = "Face recognition status already exists";
        }

        public class FaceToRecognizeConstants
        {
            public const string EntryInvalid = "Face to recognize entry is not valid.";
            public const string SuccessAdd = "Face to recognize added successfully.";
            public const string DoesNotExist = "Face to recognize does not exist.";
            public const string Exists = "Face to recognize already exists";
            public const string InvalidDate = "Date is invalid";
        }

        public class FaceExpressionConstants
        {
            public const string DoesNotExist = "Face expression does not exist.";
        }

        public class FaceToTrainConstants
        {
            public const string EntryInvalid = "Face to train entry is not valid.";
            public const string SuccessAdd = "Face to train added successfully.";
            public const string SuccessDelete = "Face to train deleted successfully.";
            public const string DoesNotExist = "Face to train does not exist.";
            public const string Exists = "Face to train already exists";
        }

        public class PersonConstants
        {
            public const string EntryInvalid = "Person entry is not valid.";
            public const string SuccessAdd = "Person added successfully.";
            public const string SuccessUpdate = "Person updated successfully.";
            public const string SuccessDelete = "Person deleted successfully.";
            public const string DoesNotExist = "Person does not exist.";
            public const string Exists = "Person already exists";
            public const string PairIdExists = "Pair id already exists";
            public const string PairIdDoesNotExist = "Pair id does not exists";
            public const string FirstNameContainsDigitsOrSpecialChar = "First name contains numbers and/or special characters";
            public const string MiddleNameContainsDigitsOrSpecialChar = "Middle name contains numbers and/or special characters";
            public const string LastNameContainsDigitsOrSpecialChar = "Last name contains numbers and/or special characters";
        }

        public class FaceRecognitionConstants
        {
            public const string SuccessTrain = "Model trained successfully.";
        }
    }
}
