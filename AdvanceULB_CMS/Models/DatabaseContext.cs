using Microsoft.EntityFrameworkCore;

namespace AdvanceULB_CMS.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<STDModel> STDs { get; set; }
        //public virtual DbSet<state> States { get; set; }
        //public virtual DbSet<Tehsil> Tehsils { get; set; }
        //public virtual DbSet<District> Districts { get; set; }

    }
}
