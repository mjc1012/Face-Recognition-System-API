namespace Face_Recognition_System_Data.Models
{
    public class ModelOutput
    {
        public string ImagePath { get; set; }

        public int Label { get; set; }

        public int PredictedLabel { get; set; }

        public float[] Score { get; set; }
    }
}
