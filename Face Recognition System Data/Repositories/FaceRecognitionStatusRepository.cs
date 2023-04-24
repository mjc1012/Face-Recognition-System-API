
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Face_Recognition_System_Data.Repositories
{
    public class FaceRecognitionStatusRepository : IFaceRecognitionStatusRepository
    {
        private readonly DataContext _context;
        public FaceRecognitionStatusRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<FaceRecognitionStatus>> RetrieveAll()
        {
            try
            {
                return await _context.FaceRecognitionStatuses.AsNoTracking().OrderBy(p => p.FaceToRecognize.LoggedTime).ThenBy(p => p.PredictedPerson.LastName).ThenBy(p => p.PredictedPerson.MiddleName).Include(p => p.PredictedPerson.LastName).Include(p => p.FaceToRecognize).Include(p => p.PredictedPerson).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(FaceRecognitionStatus status, FaceToRecognize face, Person person)
        {
            try
            {
                status.FaceToRecognize= face;
                status.PredictedPerson= person; 
                _context.FaceRecognitionStatuses.Update(status);
                await _context.SaveChangesAsync();
            }
            catch (Exception )
            {
                throw ;
            }
        }



        public async Task<bool> Exists(FaceRecognitionStatus status, FaceToRecognize face, Person person)
        {
            try
            {
                return await _context.FaceRecognitionStatuses.AnyAsync(p => p.FaceToRecognize == face && p.PredictedPerson == person && p.Id != status.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
