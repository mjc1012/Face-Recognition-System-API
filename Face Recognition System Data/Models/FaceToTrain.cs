using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Data.Models
{
    public class FaceToTrain
    {
        public int Id { get; set; }
        public string ImageFile { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int FaceExpressionId { get; set; }

        public FaceExpression FaceExpression { get; set; }

        public ICollection<AugmentedFace> AugmentedFaces { get; set; }
    }
}
