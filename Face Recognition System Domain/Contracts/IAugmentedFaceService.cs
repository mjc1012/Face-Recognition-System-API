using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IAugmentedFaceService
    {
        Task Create(AugmentedFace augmentedFace);

        Task<List<AugmentedFaceDto>> RetrieveData(int faceToTrainId);
    }
}
