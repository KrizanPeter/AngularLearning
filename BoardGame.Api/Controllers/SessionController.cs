using API.DTOs.Session;
using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        public IMapper _mapper { get; }
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public SessionController(ILogger<SessionController> logger,
            IUnitOfWork uow,
            IMapper mapper,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAppUserService appUserService)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
        }

        //  API Example : { api/users }
        [HttpPost]
        public async Task<ActionResult> AddSessionAsync([FromBody] CreateSessionDto sessionDto)
        {
            _logger.LogInformation("AddSessionInvoked");
            Session session = _mapper.Map<Session>(sessionDto);

            _uow.Sessions.Add(session);
            if (await _uow.Save())
            {
                return Ok();
            }
            return BadRequest();
        }

        //  API Example : { api/users }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            _logger.LogInformation("GetSessionInvoked");
            var sessions = await _uow.Sessions.GetAll();
            var sessionDtos = _mapper.Map<IEnumerable<SessionDto>>(sessions);
            return Ok(sessionDtos);
        }


        //  API Example : { api/user/3 }
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _uow.Sessions.Get(id);
            if (session == null)
            {
                return BadRequest("Session was not found.");
            }
            var sessionDto = _mapper.Map<SessionDto>(session);
            return Ok(sessionDto);
        }

        [Authorize]
        [HttpPost("join")]
        public async Task<ActionResult<bool>> Join(JoinToSessionDto joinDto)
        {
            if (joinDto.SessionId == 0 || string.IsNullOrEmpty(joinDto.UserName))
            {
                return BadRequest("Incorrect session or user.");
            }
            var userId = await _appUserService.GetAppUserId(User.GetUserName());
            try{
                await _appUserService.AddServiceToUserAsync(userId, joinDto.SessionId);
                return Ok(true);
            }
            catch
            {
                //Todo: replace with correct code
                return BadRequest(500);
            }
        }

    }
}