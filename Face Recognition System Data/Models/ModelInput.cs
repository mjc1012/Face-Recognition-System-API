namespace Face_Recognition_System_Data.Models
{
    public class ModelInput
    {

        public byte[] Image { get; set; }

        public UInt32 LabelAsKey { get; set; }

        public string ImagePath { get; set; }

        public int Label { get; set; }
    }
}
