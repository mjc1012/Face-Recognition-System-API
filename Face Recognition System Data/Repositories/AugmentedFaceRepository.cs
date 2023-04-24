
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Face_Recognition_System_Data.Repositories
{
    public class AugmentedFaceRepository : IAugmentedFaceRepository
    {
        private readonly DataContext _context;
        public AugmentedFaceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(AugmentedFace augmentedFace)
        {
            try
            {
                _context.AugmentedFaces.Update(augmentedFace);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AugmentedFace>> RetrieveData(int faceToTrainId)
        {
            try
            {
                return await _context.AugmentedFaces.AsNoTracking().Where(p => p.FaceToTrain.Id == faceToTrainId).Include(p => p.FaceToTrain).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
