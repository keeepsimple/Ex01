using Ex01.Models;
using Microsoft.EntityFrameworkCore;

namespace Ex01.Data
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> opt) : base(opt)
        {

        }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
