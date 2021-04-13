using AuthorizationSample.Dtos;
using AuthorizationSample.Models;
using AuthorizationSample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationSample.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserAuthorizationService _userAuthorizationService;

        public LoginController(IUserAuthorizationService userAuthorizationService)
        {
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserCredentialDataDto userCredentialDataDto)
        {
            var loginResult = _userAuthorizationService.Login(userCredentialDataDto);

            if (!loginResult.IsSuccessful)
            {
                return BadRequest();
            }

            return Ok(loginResult.Token);
        }
    }
}
