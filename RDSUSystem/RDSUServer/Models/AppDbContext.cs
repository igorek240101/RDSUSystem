using Microsoft.EntityFrameworkCore;

namespace RDSUServer.Models
{
    public class AppDbContext : DbContext
    {
        public static AppDbContext db = new AppDbContext();

        public DbSet<Users> Users { get; set; }

        public DbSet<Athletes> Athletes { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Ch_FullNames> Ch_FullNames { get; set; }

        public DbSet<Ch_LogIns> Ch_LogIns { get; set; }

        public DbSet<Ch_Partnerships> Ch_Partnerships { get; set; }

        public DbSet<Ch_Transfers> Ch_Transfers { get; set; }

        public DbSet<Disqualificatons> Disqualificatons { get; set; }

        public DbSet<GoodResults> GoodResults { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<RDSUWorkers> RDSUWorkers { get; set; }

        public DbSet<Scorecards> Scorecards { get; set; }

        public DbSet<Tournaments> Tournaments { get; set; }

        public DbSet<Trainers> Trainers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Filename=base.db");
        }
    }
}
