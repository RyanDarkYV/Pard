using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pard.Application.Common.Interfaces;

namespace Pard.WebApi.Controllers
{
    [Authorize]
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class LocationsController : BaseController
    {
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }
        /// <summary>
        /// Returns an Enumerable of locations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLocationsForActiveRecords()
        {
            var userId = Guid.Parse(GetUserId());
            var result = _locationsService.GetLocationsForActiveRecords(userId);

            return new OkObjectResult(result);
        }
    }
}