using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pard.Application.ViewModels;
using System;
using System.Threading.Tasks;
using Pard.Application.Common.Interfaces;

namespace Pard.WebApi.Controllers
{
    [Authorize]
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class RecordsController : BaseController
    {
        private readonly IRecordsService _recordsService;

        public RecordsController(IRecordsService recordsService)
        {
            _recordsService = recordsService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllFinishedRecordsForUser()
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _recordsService.GetAllFinishedRecordsForUser(userId);
            
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
            var result = await _recordsService.GetRecordByTitle(title, userId);

            if (result == null)
            {
                return new NotFoundResult();
            }

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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var recordId = Guid.Parse(id);
            var userId = Guid.Parse(GetUserId());
            await _recordsService.SoftDeleteRecord(recordId, userId);
            return new OkResult();
        }
    }
}