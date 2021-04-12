using AuthorizationSample.Models;
using AuthorizationSample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationSample.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/authorize")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserAuthorizationService _userAuthorizationService;

        public AuthorizationController(IUserAuthorizationService userAuthorizationService)
        {
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserCredentialData userCredentialData)
        {
            var token = _userAuthorizationService.Login(userCredentialData);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            return Ok(token);
        }
    }
}
