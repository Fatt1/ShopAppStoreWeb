using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.ComboTypes.DTOs;
using ShopAppStore.Application.Features.ComboTypes.Queries;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/combotype")]
    [ApiController]
    public class ComboTypeController : ApiController
    {
        public ComboTypeController(ISender sender) : base(sender)
        {
        }



        [HttpGet("all")]
        public async Task<ActionResult<List<GetAllComboTypeDTO>>> GetAllComboTypes(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetAllComboTypeQuery(), cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
    }
}
