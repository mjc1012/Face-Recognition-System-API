using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using System.Drawing;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IImageService
    {
        string SaveImage(string base64String, int personId);
        string SaveAugmentedImage(Bitmap image, int personId);
        void DeleteImage(FaceToTrainDto face, List<AugmentedFaceDto> augmentedFaces);

        void DeleteImage(FaceToRecognizeDto face);

        void DeleteFolder(int id);
        string ImagePathToBase64(string path);
    }
}
