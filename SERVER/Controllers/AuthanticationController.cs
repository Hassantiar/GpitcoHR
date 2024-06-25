using Base.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serverlibrary.Repository.Contact;

namespace SERVER.Controllers
{
    [Route("api/Authantication")]
    [ApiController]
    [AllowAnonymous]
    public class AuthanticationController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("requst")]
        public async Task<IActionResult> CreateAsync(Reguster user)
        {
            if (user == null) return BadRequest("Model is empty");
            var result=await userAccount.CreateAsync(user);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult>SignInAsync(Login user)
        {
            if (user == null) return BadRequest("Modil is empty");
            var result = await userAccount.SingInAsync(user);
            return Ok(result);
        }
        [HttpPost("refresh-Token")]
        public async Task<IActionResult> REfreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model is Empty");
            var result = await userAccount.RefrashInfoAsync(token);
            return Ok(result);
        }
    }
}
