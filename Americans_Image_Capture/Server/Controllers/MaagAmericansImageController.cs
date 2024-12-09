using Americans_Image_Capture.Server.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Americans_Image_Capture.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaagAmericansImageController : ControllerBase
    {
        private readonly IMaagAmericansImageRepository _repository;

        public MaagAmericansImageController(IMaagAmericansImageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<MaagAmericansImage>>> GetAll()
        {
            var images = await _repository.GetAllAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaagAmericansImage>> GetById(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(image);
        }


        [HttpGet("GetCurrentUsername")]
        public IActionResult GetCurrentUsername()
        {
            try
            {
                var username = Environment.UserName; // Fetch the system username.
                return Ok(username);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("maagdata")]
        public async Task<ActionResult> Add(MaagAmericansImage image)
        {
            // Set the MAAGImage navigation property for each Imagedata object
            foreach (var imageData in image.Images)
            {
                imageData.MAAGImage = image;  // Link the imageData to the parent MaagAmericansImage
                imageData.MAAGImageId = image.Id;
            }

            await _repository.AddAsync(image);
            return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, MaagAmericansImage image)
        {
            if (id != image.Id)
                return BadRequest();

            await _repository.UpdateAsync(image);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    
}
}
