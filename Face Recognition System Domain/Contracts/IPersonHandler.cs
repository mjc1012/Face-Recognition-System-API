
using Face_Recognition_System_Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Contracts
{
    public interface IPersonHandler
    {
        Task<List<string>> CanAdd(PersonDto person);
        Task<List<string>> CanUpdate(PersonDto person);
        Task<List<string>> CanDelete(int id);
        Task<List<string>> CanDelete(DeleteRangeDto deleteRange);
    }
}
