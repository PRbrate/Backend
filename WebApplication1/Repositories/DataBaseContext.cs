using Microsoft.EntityFrameworkCore;
using OngTDE.BackEnd.Models;

namespace OngTDE.BackEnd.Repositories
{
    public class DataBaseContext : DbContext
    {
        //construção do banco através de migrations
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
           : base(options)
        {

        }

        public DbSet<User> Usuarios { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(100);

            configurationBuilder
              .Properties<decimal>()
              .HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);
        }
    }
}
