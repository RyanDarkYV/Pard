using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pard.Application.Records.Commands.DeleteRecord;
using Pard.Application.Records.Commands.RestoreRecord;
using Pard.Application.Records.Queries.GetArchivedRecordsQuery;
using System;
using System.Threading.Tasks;

namespace Pard.WebApi.Controllers
{
    [Authorize]
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class ArchiveController : BaseController
    {
        private readonly IMediator _mediator;

        public ArchiveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _mediator.Send(new GetArchivedRecordsQuery {UserId = userId});
            
            return new OkObjectResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Restore([FromBody] RestoreRecordCommand command)
        {
            var userId = Guid.Parse(GetUserId());
            command.UserId = userId;
            await _mediator.Send(command);
            
            return new OkResult();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var recordId = Guid.Parse(id);
            var userId = Guid.Parse(GetUserId());
            var command = new DeleteRecordCommand {Id = recordId, UserId = userId};
            await _mediator.Send(command);
            return new OkResult();
            
        }

    }
}