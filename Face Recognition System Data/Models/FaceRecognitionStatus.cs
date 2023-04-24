using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Data.Models
{
    public class FaceRecognitionStatus
    {
        public int Id { get; set; }
        public bool IsRecognized { get; set; }
        public int FaceToRecognizeId { get; set; }
        public FaceToRecognize FaceToRecognize { get; set; }

        public int PredictedPersonId { get; set; }

        public Person PredictedPerson { get; set; }
    }
}
