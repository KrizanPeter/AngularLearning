using API.DTOs.Session;
using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly ISessionService _sessionService;

        public SessionController(ILogger<SessionController> logger,
            IMapper mapper,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAppUserService appUserService,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
            _sessionService = sessionService;
        }

        //  API Example : { api/users }
        [HttpPost]
        public async Task<ActionResult> AddSessionAsync([FromBody] CreateSessionDto sessionDto)
        {
            _logger.LogInformation("AddSessionInvoked");
            
            if(string.IsNullOrEmpty(sessionDto.SessionName))
            {
                return BadRequest("Session name can not be empty.");
            }

            SessionModel session = _mapper.Map<SessionModel>(sessionDto);

            var userId = await _appUserService.GetAppUserId(User.GetUserName());
            var result = await _sessionService.AddSession(userId, session);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpGet("leavesession")]
        [Authorize]
        public async Task<ActionResult> LeaveSessionAsync()
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());
            if(user == null)
            {
                return BadRequest("User not found.");
            }
            var result = await _appUserService.LeaveSessionForUserAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest("Uups something went wrong.");
        }

        //  API Example : { api/users }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            _logger.LogInformation("GetSessionInvoked");
            var sessions = await _sessionService.GetSessions();

            if(!sessions.Succeeded)
            {
                return BadRequest(sessions.Errors);
            }

            var sessionDtos = _mapper.Map<IEnumerable<SessionDto>>(sessions.Data);
            return Ok(sessionDtos);
        }


        //  API Example : { api/user/3 }
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _sessionService.GetSessionById(id);
            if (!session.Succeeded)
            {
                return BadRequest(session.Errors);
            }
            var sessionDto = _mapper.Map<SessionDto>(session);
            return Ok(sessionDto);
        }

        [Authorize]
        [HttpPost("join")]
        public async Task<ActionResult<bool>> Join(JoinToSessionDto joinDto)
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());

            if((user.SessionId != null && user.SessionId != 0) && user.SessionId != joinDto.SessionId)
            {
                var session = await _sessionService.GetSessionById(user.SessionId ?? default(int));
                return BadRequest("You are already member of " + session.Data.SessionName);
            }

            if(user.SessionId == joinDto.SessionId)
            {
                return Ok(false);
            }
            if (joinDto.SessionId == 0 || string.IsNullOrEmpty(joinDto.UserName))
            {
                return BadRequest("Incorrect session or user.");
            }
            var userId = await _appUserService.GetAppUserId(User.GetUserName());

            var result = await _appUserService.AddSessionToUserAsync(userId, joinDto.SessionId);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(true);
        }
    }
}