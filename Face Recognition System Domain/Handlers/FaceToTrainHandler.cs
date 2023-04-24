using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Domain.Contracts;
using Face_Recognition_System_Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_Domain.Handlers
{
    public class FaceToTrainHandler : IFaceToTrainHandler
    {
        private readonly IFaceToTrainService _faceToTrainService;
        private readonly IPersonService _personService;
        private readonly IFaceExpressionService _faceExpressionService;
        public FaceToTrainHandler(IFaceToTrainService faceToTrainService, IFaceExpressionService faceExpressionService, IPersonService personService) 
        {
            _faceToTrainService = faceToTrainService;
            _faceExpressionService = faceExpressionService;
            _personService = personService;
        }

        public async Task<List<string>> CanAdd(FaceToTrainDto face)
        {
            var validationErrors = new List<string>();

            if (face == null)
            {
                validationErrors.Add(AugmentedFaceConstants.EntryInvalid);
            }
            else
            {
                PersonDto person = await _personService.FindByPairId(face.PairId);
                if (person == null)
                {
                    validationErrors.Add(PersonConstants.DoesNotExist);
                }

                FaceExpressionDto expresson = await _faceExpressionService.Find(face.FaceExpressionId);
                if (expresson == null)
                {
                    validationErrors.Add(FaceExpressionConstants.DoesNotExist);
                }

                if (await _faceToTrainService.Exists(face, person,expresson))
                {
                    validationErrors.Add(FaceToTrainConstants.Exists);
                }
            }

            return validationErrors;
        }

        public async Task<List<string>> CanDelete(int id)
        {
            var validationErrors = new List<string>();

            if (await _faceToTrainService.Find(id) == null)
            {
                validationErrors.Add(AugmentedFaceConstants.EntryInvalid);
            }

            return validationErrors;
        }
    }
}
