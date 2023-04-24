using AutoMapper;
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Face_Recognition_System_Data.Repositories;
using Face_Recognition_System_Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<List<PersonDto>> RetrieveAll()
        {
            try
            {
                return _mapper.Map<List<PersonDto>>(await _personRepository.RetrieveAll());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PersonDto> FindById(int id)
        {
            try
            {
                return _mapper.Map<PersonDto>(await _personRepository.FindById(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PersonDto> FindByPairId(int id)
        {
            try
            {
                return _mapper.Map<PersonDto>(await _personRepository.FindByPairId(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PersonDto>> RetrieveData(List<int> pairIds)
        {
            try
            {
                return _mapper.Map<List<PersonDto>>(await _personRepository.RetrieveData(pairIds));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(PersonDto person)
        {
            try
            {
                await _personRepository.Create(_mapper.Map<Person>(person));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(PersonDto person)
        {
            try
            {

                Person personFound = await _personRepository.FindByPairId(person.PairId);
                person.Id = personFound.Id;
                await _personRepository.Update(_mapper.Map<Person>(person));
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
                await _personRepository.DeleteById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteByPairId(int pairId)
        {
            try
            {
                Person person = await _personRepository.FindByPairId(pairId);
                await _personRepository.DeleteByPairId(pairId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteRangeDto deleteRange)
        {
            try
            {
                await _personRepository.Delete(deleteRange.Ids);
            }
            catch (Exception)
            {
                throw;
            }
        }


        
        public async Task<bool> NameExists(PersonDto person)
        {
            try
            {
               return await _personRepository.NameExists(_mapper.Map<Person>(person));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PairIdExists(PersonDto person)
        {
            try
            {
                return await _personRepository.PairIdExists(_mapper.Map<Person>(person));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
