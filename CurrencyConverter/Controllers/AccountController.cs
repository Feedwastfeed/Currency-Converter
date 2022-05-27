using DemoinLayer.Domin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(Register register)
        {
            var user = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                return Ok("Register Succeeded");
            }
            return NotFound("Register Failed\nPasswordRequiresNonAlphanumeric,\nPasswordRequiresDigit,\n" +
                "PasswordRequiresUpper");

        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string UserName, string Password)
        {
            
            var result = await _signInManager.PasswordSignInAsync(
                UserName, Password, false, false);

            if (result.Succeeded)
            {
                return Ok("Login Succeeded");
            }
            return NotFound("Login Failed\nInvaled Username or Password");
        }
    }
}
