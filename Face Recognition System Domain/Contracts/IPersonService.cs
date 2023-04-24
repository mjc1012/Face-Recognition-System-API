using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IPersonService
    {
        Task<List<PersonDto>> RetrieveAll();

        Task<PersonDto> FindById(int id);

        Task<List<PersonDto>> RetrieveData(List<int> pairIds);

        Task<PersonDto> FindByPairId(int pairId);

        Task Create(PersonDto person);
        Task Update(PersonDto person);

        Task DeleteById(int id);
        Task DeleteByPairId(int pairId);

        Task Delete(DeleteRangeDto deleteRange);



        Task<bool> NameExists(PersonDto person);
        Task<bool> PairIdExists(PersonDto person);
    }
}
