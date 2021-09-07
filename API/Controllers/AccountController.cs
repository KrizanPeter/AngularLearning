using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories.Uow;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(ILogger<AccountController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService )
        {
            _tokenService = tokenService;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");

            var user = new AppUser()
            {
                UserName = registerDto.UserName,
                NormalizedUserName = registerDto.UserName.ToUpper()
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) { return BadRequest("Account creation failed"); }

            return new UserDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(a => a.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName == loginDto.UserName.ToLower());

            if(user == null) { return Unauthorized("Invalid username");}

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            

            if(! result.Succeeded) { return Unauthorized(); }

            return new UserDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }



    }
}
