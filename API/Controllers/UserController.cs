using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories.Uow;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _uow;

        public UserController(ILogger<UserController> logger, DataContext context, IUnitOfWork uow)
        {
            _uow = uow;
            _logger = logger;
            _dbContext = context;
        }

        //  API Example : { api/users }
        [HttpPost]
        public ActionResult AddUser(AppUser user)
        {
            _uow.AppUsers.Add(user);
            _uow.Save();
            return Ok();
        }

        //  API Example : { api/users }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _uow.AppUsers.GetAll();
            return Ok(users);
        }


        //  API Example : { api/user/3 }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetAppUsers(int id)
        {
            var user = await _uow.AppUsers.Get(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
