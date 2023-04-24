using Face_Recognition_System_Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IUnitOfWork
    {
        IAugmentedFaceService AugmentedFaceService { get; }
        IFaceExpressionService FaceExpressionService { get; }
        IFaceRecognitionService FaceRecognitionService { get; }
        IFaceRecognitionStatusService FaceRecognitionStatusService { get; }
        IFaceToRecognizeService FaceToRecognizeService { get; }
        IFaceToTrainService FaceToTrainService { get; }
        IImageAugmentationService ImageAugmentationService { get; }
        IImageService ImageService { get; }
        IPersonService PersonService { get; }
        IFaceRecognitionStatusHandler FaceRecognitionStatusHandler { get; }
        IFaceToRecognizeHandler FaceToRecognizeHandler { get; }
        IFaceToTrainHandler FaceToTrainHandler { get; }
        IPersonHandler PersonHandler { get; }
    }
}
