using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IFaceToTrainService
    {
        Task<List<FaceToTrainDto>> RetrieveAll();

        Task<List<FaceToTrainDto>> RetrieveData(int pairId);
        Task<bool> Exists(int pairId, int expressionId);

        Task<FaceExpressionDto> FindExpression(int pairId, List<FaceExpressionDto> faceExpressions);

        Task<FaceToTrainDto> Find(int id);

        Task<FaceToTrainDto> Create(FaceToTrainDto face, PersonDto person, FaceExpressionDto expression);

        Task Delete(int id);
        Task<bool> Exists(FaceToTrainDto face, PersonDto person, FaceExpressionDto expression);
    }
}
