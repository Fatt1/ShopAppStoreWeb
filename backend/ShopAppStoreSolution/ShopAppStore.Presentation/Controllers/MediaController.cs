using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Application.Features.Medias.Commands.UploadImage;

namespace ShopAppStore.Presentation.Controllers
{
    [Route("api/v1/media")]
    [ApiController]
    public class MediaController : ApiController
    {
        public MediaController(ISender sender) : base(sender)
        {
        }

        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages([FromForm] UploadProductImagesCommand request)
        {
            var result = await _sender.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}
