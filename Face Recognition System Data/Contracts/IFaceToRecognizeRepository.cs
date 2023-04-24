using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Data.Contracts
{
    public interface IFaceToRecognizeRepository
    {

        Task<FaceToRecognize> Find(int id);
        Task Create(FaceToRecognize face);
    }
}
