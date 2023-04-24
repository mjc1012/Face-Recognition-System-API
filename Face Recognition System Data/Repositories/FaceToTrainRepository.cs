
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Face_Recognition_System_Data.Repositories
{
    public class FaceToTrainRepository : IFaceToTrainRepository
    {
        private readonly DataContext _context;
        public FaceToTrainRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<List<FaceToTrain>> RetrieveAll()
        {
            try
            {
                return await _context.FacesToTrain.AsNoTracking().OrderByDescending(p => p.Id).Include(p => p.Person).Include(p => p.FaceExpression).Include(p => p.AugmentedFaces).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<FaceToTrain>> RetrieveData(int pairId)
        {
            try
            {
                return await _context.FacesToTrain.AsNoTracking().Where(p => p.Person.PairId == pairId).Include(p => p.Person).Include(p => p.FaceExpression).Include(p => p.AugmentedFaces).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FaceToTrain> Find(int id)
        {
            try
            {
                return await _context.FacesToTrain.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Exists(int pairId, int expressionId)
        {
           
            try
            {
                return await _context.FacesToTrain.AnyAsync(p => p.Person.PairId == pairId && p.FaceExpression.Id == expressionId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FaceExpression> FindExpression(int pairId, List<FaceExpression> expressions)
        {
            try
            {
                foreach (FaceExpression expression in expressions)
                {
                    if (!await Exists(pairId, expression.Id))
                    {
                        return expression;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<FaceToTrain> Create(FaceToTrain face, Person person, FaceExpression expression)
        {
            try
            {
                face.Person = person;
                face.FaceExpression = expression;
                _context.FacesToTrain.Update(face);
                await _context.SaveChangesAsync();
                return face;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task Delete(int id)
        {
            try
            {
                FaceToTrain faceDelete = await Find(id);
                _context.FacesToTrain.Remove(faceDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Exists(FaceToTrain face, Person person, FaceExpression expression)
        {
            try
            {
                return await _context.FacesToTrain.AnyAsync(p => p.FaceExpression == expression && p.Person == person && p.Id != face.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
