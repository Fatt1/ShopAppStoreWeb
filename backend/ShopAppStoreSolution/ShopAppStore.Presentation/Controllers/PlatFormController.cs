using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.PlatForms.Commands;
using ShopAppStore.Application.Features.PlatForms.DTOs;
using ShopAppStore.Application.Features.PlatForms.Queries;
using ShopAppStore.Shared;
using System.Net;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/platform")]
    [ApiController]
    public class PlatFormController : ApiController
    {
        public PlatFormController(ISender sender) : base(sender)
        {
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<GetAllPlatFormDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllPlatForms()
        {
            var query = new GetAllPlatFormQuery();
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
        public async Task<IActionResult> CreatePlatForm([FromBody] CreatePlatFormCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}
