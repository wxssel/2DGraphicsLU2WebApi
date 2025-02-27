using _2DGraphicsLU2.WebApi.Models;
using _2DGraphicsLU2.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _2DGraphicsLU2.WebApi.Services;


namespace _2DGraphicsLU2.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Environment2D")]
    public class Environment2DController : ControllerBase
    {
        private readonly Environment2DRepository _environment2DRepository;
        IAuthenticationService _authenticationService;
        private readonly ILogger<Environment2DController> _logger;

        public Environment2DController(IAuthenticationService authenticationService, Environment2DRepository environment2DRepository, ILogger<Environment2DController> logger)
        {
            _authenticationService = authenticationService;
            _environment2DRepository = environment2DRepository;
            _logger = logger;
        }
        [HttpGet(Name = "ReadEnvironment2D")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Environment2D>>> Get()
        {
            
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();

            var Environment2D = await _environment2DRepository.ReadAsync(userId);
            return Ok(Environment2D);

        }

        [HttpGet("{environmnet2DId:Guid}", Name = "ReadEnvironment2DById")]
        public async Task<ActionResult<Environment2D>> Get(Guid environment2DId, string userId)
        {
            var environment2D = await _environment2DRepository.ReadAsync(environment2DId, userId);
            if (environment2D == null)
                return NotFound();

            return Ok(environment2D);
        }

        [HttpPost(Name = "CreateEnvironment2D")]
        public async Task<ActionResult> Add(Environment2D environment2D, string userId)
        {
            environment2D.Id = Guid.NewGuid();

            var createdEnvironment2D = await _environment2DRepository.InsertAsync(environment2D, userId);
            return Created();
        }

        [HttpPut("{environment2DId:Guid}", Name = "UpdateEnvironment2D")]
        public async Task<ActionResult> Update(Guid environment2DId, Environment2D newEnvironment2D, string userId)
        {
            var existingEnvironment2D = await _environment2DRepository.ReadAsync(environment2DId, userId);

            if (existingEnvironment2D == null)
                return NotFound();

            await _environment2DRepository.UpdateAsync(newEnvironment2D, userId);

            return Ok(newEnvironment2D);
        }

        [HttpDelete("{environment2DId}", Name = "DeleteEnvironment2DByDate")]
        public async Task<IActionResult> Update(Guid environment2DId, string userId)
        {
            var existingEnvironment2D = await _environment2DRepository.ReadAsync(environment2DId, userId);

            if (existingEnvironment2D == null)
                return NotFound();

            await _environment2DRepository.DeleteAsync(environment2DId, userId);

            return Ok();
        }

    }
}
