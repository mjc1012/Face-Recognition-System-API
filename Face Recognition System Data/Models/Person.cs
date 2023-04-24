using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Data.Models
{
    public class Person
    {
        public int Id { get; set; }

        public int PairId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public ICollection<FaceToTrain> FacesToTrain { get; set; }

        public ICollection<FaceRecognitionStatus> FaceRecognitionStatuses { get; set; }
    }
}
