using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pard.Application.ViewModels;

namespace Pard.Application.Interfaces
{
    public interface ILocationsService
    {
        Task<IEnumerable<LocationViewModel>> GetLocationsForActiveRecords(Guid userId);
    }
}