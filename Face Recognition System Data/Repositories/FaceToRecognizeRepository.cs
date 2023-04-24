
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Face_Recognition_System_Data.Repositories
{
    public class FaceToRecognizeRepository : IFaceToRecognizeRepository
    {
        private readonly DataContext _context;
        public FaceToRecognizeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<FaceToRecognize> Find(int id)
        {
            try
            {
                return await _context.FacesToRecognize.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(FaceToRecognize face)
        {
            try
            {
                _context.FacesToRecognize.Update(face);
                await _context.SaveChangesAsync();
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
