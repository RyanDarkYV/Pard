using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pard.Application.Interfaces;
using Pard.Domain.Entities.Locations;
using Pard.Persistence.Contexts;

namespace Pard.Persistence.Repositories.Records
{
    public class SqlServerLocationsRepository : ILocationsRepository
    {
        // TODO: Add abstraction for RecordsContext to ApplicationLayer and move repo implementation to ApplicationLayer
        private readonly RecordsContext _context;

        public SqlServerLocationsRepository(RecordsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Location>> GetLocationsForActiveRecords(Guid userId)
        {
            var result = _context.Locations.Where(x => x.Record.IsDone == false && x.Record.UserId == userId);
            return result;
        }
    }
}