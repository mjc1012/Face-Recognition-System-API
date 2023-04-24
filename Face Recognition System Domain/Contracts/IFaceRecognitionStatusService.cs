using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IFaceRecognitionStatusService
    {
        Task<List<FaceRecognitionStatusDto>> RetrieveAll();
        Task Create(FaceRecognitionStatusDto status, FaceToRecognizeDto face, PersonDto person);
        Task<bool> Exists(FaceRecognitionStatusDto status, FaceToRecognizeDto face, PersonDto person);
    }
}
