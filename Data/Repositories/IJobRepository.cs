using Ex01.Models;

namespace Ex01.Data.Repositories
{
    public interface IJobRepository
    {
        int Add(Job job);

        int Delete(Job job);

        int Update(Job job);

        Job JobGetById(int id);

        IEnumerable<Job> GetAll();
    }
}
