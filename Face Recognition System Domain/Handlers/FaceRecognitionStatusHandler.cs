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
    public class FaceRecognitionStatusHandler : IFaceRecognitionStatusHandler
    {
        private readonly IFaceRecognitionStatusService _faceRecognitionStatusService;
        private readonly IFaceToRecognizeService _faceToRecognizeService;
        private readonly IPersonService _personService;
        public FaceRecognitionStatusHandler(IFaceRecognitionStatusService faceRecognitionStatusService, IFaceToRecognizeService faceToRecognizeService,
            IPersonService personService) 
        {
            _faceRecognitionStatusService = faceRecognitionStatusService;
            _faceToRecognizeService = faceToRecognizeService;
            _personService = personService;
        }

        public async Task<List<string>> CanAdd(FaceRecognitionStatusDto status)
        {
            var validationErrors = new List<string>();

            if (status == null)
            {
                validationErrors.Add(AugmentedFaceConstants.EntryInvalid);
            }
            else
            {
                PersonDto person = await _personService.FindById(status.PredictedPersonId);
                if (person == null)
                {
                    validationErrors.Add(PersonConstants.DoesNotExist);
                }

                FaceToRecognizeDto face = await _faceToRecognizeService.Find(status.FaceToRecognizeId);
                if (face == null)
                {
                    validationErrors.Add(FaceToRecognizeConstants.DoesNotExist);
                }

                if(_faceRecognitionStatusService.Exists(status, face, person) != null)
                {
                    validationErrors.Add(FaceRecognitionStatusConstants.Exists);
                }
            }

            return validationErrors;
        }
    }
}
