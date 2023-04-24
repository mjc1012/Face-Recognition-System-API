﻿namespace Face_Recognition_System_Data.Dtos
{
    public class FaceRecognitionStatusDto
    {
        public int Id { get; set; }

        public string LoggedTime { get; set; }
        public bool IsRecognized { get; set; }

        public int FaceToRecognizeId { get; set; }

        public int PredictedPersonId { get; set; }
    }
}
