using Ex01.Helper;
using Ex01.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ex01.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobContext _context;
        
        public JobRepository(JobContext context)
        {
            _context = context;
        }

        public int Add(Job job)
        {
            _context.Jobs.Add(job);
            return _context.SaveChanges();
        }

        public int Delete(Job job)
        {
            _context.Jobs.Remove(job);
            return _context.SaveChanges();
        }

        public IEnumerable<Job> GetAll()
        {
            return _context.Jobs;
        }

        public async Task<Paginated<Job>> GetAsync(Expression<Func<Job, bool>> filter = null, 
            Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<Job> query = _context.Jobs;
            if(filter != null) query = query.Where(filter);
            if(orderBy != null) query = orderBy(query);

            return await Paginated<Job>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public Job JobGetById(int id)
        {
            var job = _context.Jobs.FirstOrDefault(x => x.Id == id);
            if(job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            return job;
        }

        public int Update(Job job)
        {
            _context.Jobs.Update(job);
            return _context.SaveChanges();
        }
    }
}
