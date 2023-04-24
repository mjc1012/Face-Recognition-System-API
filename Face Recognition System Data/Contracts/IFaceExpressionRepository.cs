using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Data.Contracts
{
    public interface IFaceExpressionRepository
    {
        Task<List<FaceExpression>> RetrieveAll();

        Task<FaceExpression> Find(int id);
    }
}
