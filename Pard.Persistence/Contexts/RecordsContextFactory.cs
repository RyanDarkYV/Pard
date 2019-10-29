using Microsoft.EntityFrameworkCore;
using Pard.Persistence.Infrastructure;

namespace Pard.Persistence.Contexts
{
    public class RecordsContextFactory : DesignTimeDbContextFactoryBase<RecordsContext>
    {
        protected override RecordsContext CreateNewInstance(DbContextOptions<RecordsContext> options)
        {
            return new RecordsContext(options);
        }
    }
}