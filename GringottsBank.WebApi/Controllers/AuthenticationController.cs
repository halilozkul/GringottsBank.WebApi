using Gringotts.Core.Authentication;
using Gringotts.Core.Authentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GringottsBank.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Token")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;

        public AuthenticationController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authentication([FromBody]UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
