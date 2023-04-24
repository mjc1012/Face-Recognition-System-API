using System.ComponentModel.DataAnnotations.Schema;

namespace Face_Recognition_System_Data.Dtos
{
    public class AugmentedFaceDto
    {
        public int Id { get; set; }
        public string ImageFile { get; set; }
        public int FaceToTrainId { get; set; }
    }
}
