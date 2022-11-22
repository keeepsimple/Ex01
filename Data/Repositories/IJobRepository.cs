using Ex01.Helper;
using Ex01.Models;
using System.Linq.Expressions;

namespace Ex01.Data.Repositories
{
    public interface IJobRepository
    {
        int Add(Job job);

        int Delete(Job job);

        int Update(Job job);

        Job JobGetById(int id);

        IEnumerable<Job> GetAll();

        Task<Paginated<Job>> GetAsync(Expression<Func<Job, bool>> filter = null,
            Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null, int pageIndex = 1, int pageSize = 10);
    }
}
