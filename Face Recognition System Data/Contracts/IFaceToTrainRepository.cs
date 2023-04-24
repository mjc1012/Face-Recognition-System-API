using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Data.Contracts
{
    public interface IFaceToTrainRepository
    {
        Task<List<FaceToTrain>> RetrieveAll();

        Task<List<FaceToTrain>> RetrieveData(int pairId);
        Task<bool> Exists(int pairId, int expressionId);

        Task<FaceExpression> FindExpression(int pairId, List<FaceExpression> expressions);

        Task<FaceToTrain> Find(int id);

        Task<FaceToTrain> Create(FaceToTrain face, Person person, FaceExpression expression);
        Task<bool> Exists(FaceToTrain face, Person person, FaceExpression expression);

        Task Delete(int id);

    }
}
