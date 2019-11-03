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
    public class ArchiveController : BaseController
    {
        private readonly IArchiveService _archiveService;

        public ArchiveController(IArchiveService archiveService)
        {
            _archiveService = archiveService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _archiveService.GetArchivedRecords(userId);
            
            return new OkObjectResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Restore([FromBody] DeleteRecordViewModel model)
        {
            var userId = Guid.Parse(GetUserId());
            var recordId = Guid.Parse(model.Id);
            await _archiveService.RestoreRecord(recordId, userId);
            
            return new OkResult();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var recordId = Guid.Parse(id);
            var userId = Guid.Parse(GetUserId());
            await _archiveService.DeleteRecord(recordId, userId);
            return new OkResult();
        }

    }
}