using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeApp.Models;
using PracticeApp.Services;

namespace PracticeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService _service;
        private readonly ILogger<AppUsersController> _logger;

        public AppUsersController(IAppUserService service, ILogger<AppUsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Retrieving all users");
                var users = await _service.GetAllAsync();
                _logger.LogInformation($"Retrieved {users.Count} users successfully");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving user with ID: {id}");
                var user = await _service.GetByIdAsync(id);
                _logger.LogInformation($"Retrieved user with ID: {id} successfully");
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"User with ID: {id} not found");
                return NotFound($"User with ID: {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with ID: {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<AppUser>> Create([FromBody] AppUser user)
        {
            try
            {
                _logger.LogInformation($"Creating new user with name: {user.Name}");
                var createdUser = await _service.CreateAsync(user);
                _logger.LogInformation($"User created successfully with ID: {createdUser.Id}");
                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating user: {user.Name}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Update(int id, [FromBody] AppUser user)
        {
            try
            {
                _logger.LogInformation($"Updating user with ID: {id}");
                await _service.UpdateAsync(id, user);
                _logger.LogInformation($"User with ID: {id} updated successfully");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"User with ID: {id} not found for update");
                return NotFound($"User with ID: {id} not found");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Invalid arguments for updating user with ID: {id}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with ID: {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting user with ID: {id}");
                await _service.DeleteAsync(id);
                _logger.LogInformation($"User with ID: {id} deleted successfully");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"User with ID: {id} not found for deletion");
                return NotFound($"User with ID: {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with ID: {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}