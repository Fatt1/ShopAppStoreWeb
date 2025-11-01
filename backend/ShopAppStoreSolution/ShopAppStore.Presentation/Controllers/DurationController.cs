using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.Durations.Commands.CreateDuration;
using ShopAppStore.Application.Features.Durations.DTOs;
using ShopAppStore.Application.Features.Durations.Queries.GetAllDuration;
using ShopAppStore.Shared;
using System.Net;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/duration")]
    [ApiController]
    public class DurationController : ApiController
    {
        public DurationController(ISender sender) : base(sender)
        {
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<GetAllDurationDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllDurations()
        {
            var query = new GetAllDurationQuery();
            var result = await _sender.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateDuration([FromBody] CreateDurationCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return CreatedAtAction(
                    nameof(CreateDuration),
                    new { id = result.Value },
                    result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}
