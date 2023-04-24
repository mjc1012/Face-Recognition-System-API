using Face_Recognition_System_Data.Models;

namespace Face_Recognition_System_Data.Contracts
{
    public interface IPersonRepository
    {
        Task<List<Person>> RetrieveAll();

        Task<Person> FindById(int id);

        Task<List<Person>> RetrieveData(List<int> pairIds);

        Task<Person> FindByPairId(int pairId);

        Task Create(Person person);

        Task Update(Person person);

        Task DeleteById(int id);
        Task DeleteByPairId(int pairId);

        Task Delete(List<int> ids);

        Task<bool> NameExists(Person person);
        Task<bool> PairIdExists(Person person);
    }
}
