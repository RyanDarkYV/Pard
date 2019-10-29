using Pard.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pard.Application.Services
{
    public interface ILocationsService
    {
        Task<IEnumerable<LocationViewModel>> GetLocationsForActiveRecords(Guid userId);
    }
}