
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Face_Recognition_System_Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> RetrieveAll()
        {
            try
            {
                return await _context.Persons.AsNoTracking().OrderBy(p => p.LastName).ThenBy(p => p.MiddleName).ThenBy(p => p.FirstName).Include(p => p.FacesToTrain).Include(p => p.FaceRecognitionStatuses).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Person>> RetrieveData(List<int> pairIds)
        {
            try
            {
                return await _context.Persons.AsNoTracking().Where(p => pairIds.Contains(p.PairId)).OrderBy(p => p.LastName).ThenBy(p => p.MiddleName).ThenBy(p => p.FirstName).Include(p => p.FacesToTrain).Include(p => p.FaceRecognitionStatuses).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> FindById(int id)
        {
            try
            {
                return await _context.Persons.AsNoTracking().Where(p => p.Id == id).Include(p => p.FacesToTrain).Include(p => p.FaceRecognitionStatuses).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> FindByPairId(int pairId) 
        {
            try
            {
                return await _context.Persons.AsNoTracking().Where(p => p.PairId == pairId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Person person)
        {
            try
            {
                _context.Persons.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Person person)
        {
            try
            {
                _context.Persons.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task DeleteByPairId(int pairId)
        {
            try
            {
                Person personDelete = await FindByPairId(pairId);
                _context.Persons.Remove(personDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteById(int id)
        {
            try
            {
                Person personDelete = await FindById(id);
                _context.Persons.Remove(personDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(List<int> ids)
        {
            try
            {
                List<Person> peopleDelete = await RetrieveData(ids);
                _context.Persons.RemoveRange(peopleDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> NameExists(Person person)
        {
            try
            {
                return await _context.Persons.AnyAsync(p => p.FirstName == person.FirstName && p.MiddleName == person.MiddleName && p.LastName == person.LastName && p.PairId != person.PairId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PairIdExists(Person person)
        {
            try
            {
                return await _context.Persons.AnyAsync(p => p.PairId == person.PairId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
