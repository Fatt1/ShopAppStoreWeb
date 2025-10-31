using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.Categories.Commands.CreateCategory;
using ShopAppStore.Application.Features.Categories.DTOs;
using ShopAppStore.Application.Features.Categories.Queries;
using ShopAppStore.Shared;
using System.Net;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ApiController
    {
        public CategoryController(ISender sender) : base(sender)
        {
        }

        [ProducesResponseType(typeof(CreateCateogoryDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [HttpPost("create")]
        public async Task<ActionResult<CreateCateogoryDTO>> CreateCategory(CreateCategoryCommand request)
        {
            var result = await _sender.Send(request);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.Error);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<GetAllCategoryDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetAllCategoryDTO>>> GetAllCategories()
        {
            var query = new GetAllCategoryQuery();
            var result = await _sender.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

    }
}
