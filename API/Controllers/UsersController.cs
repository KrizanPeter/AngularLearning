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
    public class SessionController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<SessionController> _logger;
        private readonly IUnitOfWork _uow;

        public SessionController(ILogger<SessionController> logger, DataContext context, IUnitOfWork uow)
        {
            _uow = uow;
            _logger = logger;
            _dbContext = context;
        }

        //  API Example : { api/users }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            _logger.LogInformation("GetSessionInvoked");
            var users = await _uow.Sessions.GetAll();
            return Ok(users);
        }

        
        //  API Example : { api/user/3 }
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            var user =  await _uow.Sessions.Get(id);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

    }
}