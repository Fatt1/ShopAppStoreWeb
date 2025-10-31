using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Features.Products.Queries.FilterAppPagination;
using ShopAppStore.Application.Features.Products.Queries.GetAllAppPagination;
using ShopAppStore.Shared;
using System.Net;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/app")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ISender _sender;
        public AppController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("all-pagination")]
        [ProducesResponseType(typeof(PagedList<GetAllAppDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PagedList<GetAllAppDTO>>> GetAllAppsPagination(
           [FromQuery] GetAllAppPaginationQuery request)
        {
            var result = await _sender.Send(request);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        [HttpGet("filter")]
        public async Task<IActionResult> FilterApps([FromQuery] FilterAppPaginationQuery request)
        {
            var result = await _sender.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }
}
