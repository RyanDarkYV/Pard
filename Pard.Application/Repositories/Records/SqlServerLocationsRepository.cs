using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pard.Application.Common.Interfaces;
using Pard.Domain.Entities.Locations;

namespace Pard.Application.Repositories.Records
{
    public class SqlServerLocationsRepository : ILocationsRepository
    {
        private readonly IRecordsContext _context;

        public SqlServerLocationsRepository(IRecordsContext context)
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