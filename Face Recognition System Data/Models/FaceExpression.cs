using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Data.Models
{
    public class FaceExpression
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageFile { get; set; }

        public ICollection<FaceToTrain> FacesToTrain { get; set; }
    }
}
