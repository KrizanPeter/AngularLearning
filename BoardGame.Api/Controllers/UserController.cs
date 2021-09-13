using API.Entities.Context;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<UserController> _logger;
        private readonly IAppUserService _appUserService;

        public UserController(ILogger<UserController> logger, IAppUserService appUserService, DataContext context)
        {
            _logger = logger;
            _dbContext = context;
            _appUserService = appUserService;
        }

        //  API Example : { api/users }
        [HttpPost]
        public async Task<ActionResult> AddUserAsync(AppUser user)
        {
            var result = await _appUserService.AddAppUser(user);
            if(result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        //  API Example : { api/user/3 }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetAppUsers(int id)
        {
            var user = await _appUserService.GetAppUser(id);

            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
