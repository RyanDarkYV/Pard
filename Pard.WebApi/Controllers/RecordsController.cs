using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pard.Application.Records.Commands.CreateRecord;
using Pard.Application.Records.Commands.SoftDeleteRecord;
using Pard.Application.Records.Commands.UpdateRecord;
using Pard.Application.Records.Queries.GetRecordQuery;
using Pard.Application.Records.Queries.GetRecordsQuery;
using System;
using System.Threading.Tasks;

namespace Pard.WebApi.Controllers
{
    [Authorize]
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class RecordsController : BaseController
    {
        private readonly IMediator _mediator;

        public RecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Returns all finished records.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllFinishedRecordsForUser()
        {
            var userId = Guid.Parse(GetUserId());

            var query = new GetRecordsQuery
            {
                IsFinished = true,
                UserId = userId
            };
            var result = await _mediator.Send(query);
            
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Returns all unfinished records.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUnfinishedRecordsForUser()
        {
            var userId = Guid.Parse(GetUserId());

            var query = new GetRecordsQuery
            {
                IsFinished = false,
                UserId = userId
            };
            var result = await _mediator.Send(query);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Return a record with specified Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(Guid id)
        {
            var userId = Guid.Parse(GetUserId());

            var query = new GetRecordQuery
            {
                Id = id,
                UserId = userId
            };

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }
        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecordCommand command)
        {
            var userId = Guid.Parse(GetUserId());
            command.UserId = userId.ToString();
            await _mediator.Send(command);
            return new OkResult();
        }
        /// <summary>
        /// Updates a record.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRecordCommand command)
        {
            var userId = Guid.Parse(GetUserId());
            command.UserId = userId.ToString();
            await _mediator.Send(command);
            return new OkResult();
        }
        /// <summary>
        /// Moves record to an archive. (Soft delete)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var recordId = Guid.Parse(id);
            var userId = Guid.Parse(GetUserId());

            var command = new SoftDeleteRecordCommand
            {
                Id = recordId,
                UserId = userId
            };
            await _mediator.Send(command);

            return new OkResult();
        }
    }
}