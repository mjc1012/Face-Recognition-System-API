using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Data.Models
{
    public class FaceToRecognize
    {
        public int Id { get; set; }
        public string ImageFile { get; set; }

        public DateTime LoggedTime { get; set; }

        public FaceRecognitionStatus FaceRecognitionStatus { get; set; }
    }
}
