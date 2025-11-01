using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.Products.Commands;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Features.Products.Queries.FilterAppPagination;
using ShopAppStore.Application.Features.Products.Queries.GetAllAppPagination;
using ShopAppStore.Application.Features.Products.Queries.GetAppById;
using ShopAppStore.Application.Features.Products.Queries.GetRating;
using ShopAppStore.Shared;
using System.Net;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/app")]
    [ApiController]
    public class AppController : ApiController
    {

        public AppController(ISender sender) : base(sender)
        {
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

        [HttpGet("rating/{id}")]
        [ProducesResponseType(typeof(GetAllRatingDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAllRatingDTO>> GetAllRatings([FromRoute] Guid id)
        {
            var query = new GetAllRatingQuery(id);
            var result = await _sender.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetAppDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAppDTO>> GetAppById([FromRoute] Guid id)
        {
            var query = new GetAppByIdQuery(id);
            var result = await _sender.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
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

        [HttpPost("add")]
        [ProducesResponseType(typeof(CreateAppDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateApp([FromBody] CreateAppCommand command)
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
