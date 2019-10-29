using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pard.Application.Services;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;

namespace Pard.WebApi.Controllers
{
    [Authorize]
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class RecordsController : BaseController
    {
        private readonly IRecordsService _recordsService;
        private UserManager<AppUser> _userManager;

        public RecordsController(IRecordsService recordsService, UserManager<AppUser> userManager)
        {
            _recordsService = recordsService;
            _userManager = userManager;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetAllRecordsForUser()
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _recordsService.GetAllRecordForUser(userId);
            
            //if (result == null || !result.Any())
            //{
            //    return new NotFoundResult();
            //}
            
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnfinishedRecordsForUser()
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _recordsService.GetAllRecordInWorkForUser(userId);

            return new OkObjectResult(result);
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetRecord(string title)
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _recordsService.GetRecord(title, userId);

            //if (result == null)
            //{
            //    return new NotFoundResult();
            //}

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecordViewModel model)
        {
            var userId = Guid.Parse(GetUserId());
            model.UserId = userId.ToString();
            await _recordsService.CreateRecord(model);
            return new OkResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RecordViewModel model)
        {
            var userId = Guid.Parse(GetUserId());
            model.UserId = userId.ToString();
            await _recordsService.UpdateRecord(model);
            return new OkObjectResult(model);
        }
    }
}