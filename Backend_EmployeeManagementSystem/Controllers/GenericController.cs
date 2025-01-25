using Backend_Library.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : Controller where T : class
    {
        private readonly IGenericRepositoryInterface<T> _genericRepositoryInterface;
        private readonly ILogger<GenericController<T>> _logger;

        public GenericController(
            IGenericRepositoryInterface<T> genericRepositoryInterface,
            ILogger<GenericController<T>> logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface ?? throw new ArgumentNullException(nameof(genericRepositoryInterface));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all records of type {EntityType}", typeof(T).Name);
            try
            {
                var result = await _genericRepositoryInterface.GetAll();
                _logger.LogInformation("Successfully fetched all records of type {EntityType}", typeof(T).Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all records of type {EntityType}", typeof(T).Name);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID received for deletion: {Id}", id);
                return BadRequest("Gửi yêu cầu không hợp lệ!");
            }

            _logger.LogInformation("Attempting to delete entity with ID: {Id}", id);
            try
            {
                var result = await _genericRepositoryInterface.DeleteById(id);
                _logger.LogInformation("Successfully deleted entity with ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting entity with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID received for retrieval: {Id}", id);
                return BadRequest("Gửi yêu cầu không hợp lệ!");
            }

            _logger.LogInformation("Fetching entity with ID: {Id}", id);
            try
            {
                var result = await _genericRepositoryInterface.GetById(id);
                _logger.LogInformation("Successfully fetched entity with ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching entity with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
         {
            if (model == null)
            {
                _logger.LogWarning("Null model received for addition");
                return BadRequest("Yêu cầu không hợp lệ!");
            }

            _logger.LogInformation("Attempting to add a new entity of type {EntityType}", typeof(T).Name);
            try
            {
                var result = await _genericRepositoryInterface.Insert(model);
                _logger.LogInformation("Successfully added a new entity of type {EntityType}", typeof(T).Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new entity of type {EntityType}", typeof(T).Name);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if (model == null)
            {
                _logger.LogWarning("Null model received for update");
                return BadRequest("Yêu cầu không hợp lệ!");
            }

            _logger.LogInformation("Attempting to update an entity of type {EntityType}", typeof(T).Name);
            try
            {
                var result = await _genericRepositoryInterface.Update(model);
                _logger.LogInformation("Successfully updated an entity of type {EntityType}", typeof(T).Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an entity of type {EntityType}", typeof(T).Name);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
