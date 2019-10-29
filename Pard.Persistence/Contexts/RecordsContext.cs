using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pard.Application.Interfaces;
using Pard.Domain.Entities.Locations;
using Pard.Domain.Entities.Records;

namespace Pard.Persistence.Contexts
{
    public class RecordsContext : DbContext
    {
        public RecordsContext(DbContextOptions<RecordsContext> options) : base(options)
        { }

        public DbSet<Record> Records { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecordsContext).Assembly);
        }
    }
}