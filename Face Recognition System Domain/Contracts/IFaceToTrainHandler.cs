using Face_Recognition_System_Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IFaceToTrainHandler
    {
        Task<List<string>> CanAdd(FaceToTrainDto face);
        Task<List<string>> CanDelete(int id);
    }
}
