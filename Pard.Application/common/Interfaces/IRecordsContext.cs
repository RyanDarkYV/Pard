using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pard.Domain.Entities.Locations;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Common.Interfaces
{
    public interface IRecordsContext
    {
        DbSet<Record> Records { get; set; }
        DbSet<Location> Locations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}