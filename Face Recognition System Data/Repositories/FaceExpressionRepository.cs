
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Face_Recognition_System_Data.Repositories
{
    public class FaceExpressionRepository : IFaceExpressionRepository
    {
        private readonly DataContext _context;
        public FaceExpressionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<FaceExpression>> RetrieveAll()
        {
            try
            {
                return await _context.FaceExpressions.AsNoTracking().OrderBy(p => p.Id).Include(p => p.FacesToTrain).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FaceExpression> Find(int id)
        {
            try
            {
                return await _context.FaceExpressions.AsNoTracking().Where(p => p.Id == id).Include(p => p.FacesToTrain).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
