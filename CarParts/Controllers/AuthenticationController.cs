using AutoMapper;
using CarParts.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParts.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IRepositoryManager _repository;
        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager,
            IRepositoryManager repository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _repository = repository;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                    _logger.LogWarn($"Error: {error.Code}, {error.Description}");
                    _repository.Logs.AddLog(new Log { Type = "Warn", Action = "Registry", Message = $"Error: {error.Code}, {error.Description}" });

                    await _repository.SaveAsync();
                }
                
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            _logger.LogInfo($"Сreated a new user");
            _repository.Logs.AddLog(new Log { Type = "Info", Action = "Registry", Message = $"Сreated a new user" });
            await _repository.SaveAsync();

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authetication failed. Wrong user name or password.");
                _repository.Logs.AddLog(new Log { Type = "Info", Action = "Autheticate", Message = $"{nameof(Authenticate)}: Authetication failed. Wrong user name or password." });
                await _repository.SaveAsync();
                
                return Unauthorized();
            }
            _logger.LogInfo($"Authetication succeeded");
            _repository.Logs.AddLog(new Log { Type = "Info", Action = "Registry", Message = $"Authetication succeeded" });
            await _repository.SaveAsync();

            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}
