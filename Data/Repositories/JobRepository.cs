using Ex01.Models;

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
            return _context.Jobs.ToList();
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
