﻿using _2DGraphicsLU2.WebApi.Models;
using _2DGraphicsLU2.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _2DGraphicsLU2.WebApi.Services;


namespace _2DGraphicsLU2.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Object2D")]
    public class Object2DController : ControllerBase
    {
        private readonly Object2DRepository _object2DRepository;
        IAuthenticationService _authenticationService;

        private readonly ILogger<Object2DController> _logger;

        public Object2DController(IAuthenticationService authenticationService, Object2DRepository object2DRepository, ILogger<Object2DController> logger)
        {
            _authenticationService = authenticationService;
            _object2DRepository = object2DRepository;
            _logger = logger;
        }

        [HttpGet(Name = "ReadObjects2D")]
        public async Task<ActionResult<IEnumerable<Object2D>>> Get()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            var Objects2D = await _object2DRepository.ReadAsync(userId);
            return Ok(Objects2D);
        }

        [HttpGet("{object2DId}", Name = "ReadObject2D")]
        public async Task<ActionResult<Object2D>> Get(Guid object2DId, string userId)
        {
            var object2D = await _object2DRepository.ReadAsync(object2DId, userId);
            if (object2D == null)
                return NotFound();

            return Ok(object2D);
        }

        [HttpPost(Name = "CreateObject2D")]
        public async Task<ActionResult> Add(Object2D object2D, string userId)
        {
            object2D.Id = Guid.NewGuid();

            var createdObject2D = await _object2DRepository.InsertAsync(object2D, userId);
            return Created();
        }

        [HttpPut("{object2DId}", Name = "UpdateObject2D")]
        public async Task<ActionResult> Update(Guid object2DId, Object2D newObject2D, string userId)
        {
            var existingObject2D = await _object2DRepository.ReadAsync(object2DId, userId);

            if (existingObject2D == null)
                return NotFound();

            await _object2DRepository.UpdateAsync(newObject2D, userId);

            return Ok(newObject2D);
        }

        [HttpDelete("{object2DId}", Name = "DeleteObject2DByDate")]
        public async Task<IActionResult> Update(Guid object2DId, string userId)
        {
            var existingObject2D = await _object2DRepository.ReadAsync(object2DId, userId);

            if (existingObject2D == null)
                return NotFound();

            await _object2DRepository.DeleteAsync(object2DId, userId);

            return Ok();
        }

    }
}
 