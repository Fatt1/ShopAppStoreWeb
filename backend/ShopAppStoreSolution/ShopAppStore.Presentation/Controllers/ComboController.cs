using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.Combos.Commands.CreateCombo;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/conbo")]
    [ApiController]
    public class ComboController : ApiController
    {
        public ComboController(ISender sender) : base(sender)
        {
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateCombo([FromBody] CreateComboCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllCombos(CancellationToken cancellationToken)
        {
            var query = new Application.Features.Combos.Queries.GetAllComboQuery();
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
