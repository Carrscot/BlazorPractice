using Microsoft.AspNetCore.Mvc;
using PracticeApp.Models;
using PracticeApp.Services;

namespace PracticeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService _service;

        public AppUsersController(IAppUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AppUser>> Create([FromBody] AppUser user)
        {
            var createdUser = await _service.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppUser user)
        {
            try
            {
                await _service.UpdateAsync(id, user);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}