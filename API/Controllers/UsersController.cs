using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories.Uow;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _uow;

        public UsersController(ILogger<UsersController> logger, DataContext context, IUnitOfWork uow)
        {
            _uow = uow;
            _logger = logger;
            _dbContext = context;
        }

        //  API Example : { api/users }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _uow.AppUser.GetAll();
            return Ok(users);
        }

        
        //  API Example : { api/user/3 }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user =  await _uow.AppUser.Get(id);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }


    }
}