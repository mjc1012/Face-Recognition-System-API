using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_Domain.Handlers
{
    public class FaceToRecognizeHandler : IFaceToRecognizeHandler
    {
        public FaceToRecognizeHandler() { }

        public List<string> CanAdd(FaceToRecognizeDto face)
        {
            var validationErrors = new List<string>();

            if (face == null)
            {
                validationErrors.Add(AugmentedFaceConstants.EntryInvalid);
            }
            else
            {
                if (!DateTime.TryParseExact(face.LoggedTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    validationErrors.Add(FaceToRecognizeConstants.InvalidDate);
                }
            }


            return validationErrors;
        }
    }
}
