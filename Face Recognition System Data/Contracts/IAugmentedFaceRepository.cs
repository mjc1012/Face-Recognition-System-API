using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Data.Contracts
{
    public interface IAugmentedFaceRepository
    {
        Task Create(AugmentedFace augmentedFace);

        Task<List<AugmentedFace>> RetrieveData(int faceToTrainId);
    }
}
