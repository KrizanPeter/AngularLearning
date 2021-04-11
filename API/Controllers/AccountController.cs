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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;

        public AccountController(ILogger<AccountController> logger, DataContext context, ITokenService tokenService )
        {
            _tokenService = tokenService;
            _logger = logger;
            _dbContext = context;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                UserName = registerDto.UserName.ToLower(),
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new UserDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _dbContext.Users.AnyAsync(a => a.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.UserName == loginDto.UserName);

            if(user == null) { return Unauthorized("Invalid username");}


            return new UserDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }



    }
}
