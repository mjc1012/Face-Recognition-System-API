using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using OpenCvSharp;
using System.Drawing;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IImageAugmentationService
    {
        Task RunImageAugmentation(FaceToTrainDto face);
    }
}
