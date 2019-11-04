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
        /// <summary>
        /// Returns all archived records for user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(GetUserId());
            var result = await _mediator.Send(new GetArchivedRecordsQuery {UserId = userId});
            
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Removes a record from archive. Sets IsDeleted flag to false.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Restore([FromBody] RestoreRecordCommand command)
        {
            var userId = Guid.Parse(GetUserId());
            command.UserId = userId;
            await _mediator.Send(command);
            
            return new OkResult();
        }
        /// <summary>
        /// Deletes a record from archive.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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