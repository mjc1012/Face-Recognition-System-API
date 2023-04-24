using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Data.Contracts
{
    public interface IFaceRecognitionStatusRepository
    {
        Task<List<FaceRecognitionStatus>> RetrieveAll();
        Task Create(FaceRecognitionStatus status, FaceToRecognize face, Person person);

        Task<bool> Exists(FaceRecognitionStatus status, FaceToRecognize face, Person person);
    }
}
