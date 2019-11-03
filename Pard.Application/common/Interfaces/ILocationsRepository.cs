using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pard.Domain.Entities.Locations;

namespace Pard.Application.Common.Interfaces
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetLocationsForActiveRecords(Guid userId);
    }
}