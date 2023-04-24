using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Face_Recognition_System_Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaceExpression>().HasData(
                new FaceExpression
                {
                   Id= 1,
                    Name = "Resting Face",
                    ImageFile = "face_1.jpg"
                },
                new FaceExpression
                {
                    Id = 2,
                    Name = "Resting Face with Eyes Closed",
                    ImageFile = "face_2.jpg"
                },
                new FaceExpression
                {
                    Id = 3,
                    Name = "Open Mouth Face",
                    ImageFile = "face_3.jpg"
                },
                new FaceExpression
                {
                    Id = 4,
                    Name = "Open Mouth Face with Eyes Closed",
                    ImageFile = "face_4.jpg"
                },
                new FaceExpression
                {
                    Id = 5,
                    Name = "Smiling Face",
                    ImageFile = "face_5.jpg"
                }, new FaceExpression
                {
                    Id = 6,
                    Name = "Smiling Face with Eyes Closed",
                    ImageFile = "face_6.jpg"
                },
                new FaceExpression
                {
                    Id = 7,
                    Name = "Duck Face",
                    ImageFile = "face_7.jpg"
                },
                new FaceExpression
                {
                    Id = 8,
                    Name = "Duck Face with Eyes Closed",
                    ImageFile = "face_8.jpg"
                });

            modelBuilder.Entity<Person>().HasData(
              new Person
              {
                  Id = 1,
                  FirstName = "ADMIN",
                  MiddleName = "",
                  LastName = "ADMIN",
                  PairId = 1
              });
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<FaceExpression> FaceExpressions { get; set; }

        public DbSet<FaceToTrain> FacesToTrain { get; set; }

        public DbSet<AugmentedFace> AugmentedFaces { get; set; }

        public DbSet<FaceToRecognize> FacesToRecognize { get; set; }

        public DbSet<FaceRecognitionStatus> FaceRecognitionStatuses { get; set; }
    }
}
