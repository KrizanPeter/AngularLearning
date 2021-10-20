using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using BoardGame.Api.DTOs.Block;
using BoardGame.Api.DTOs.CoreGame;
using BoardGame.Api.DTOs.Hero;
using BoardGame.Api.DTOs.Session;
using BoardGame.Domain.Models.Enums;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGame.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CoreGameController : ControllerBase
    {
        private readonly ILogger<CoreGameController> _logger;
        public IMapper _mapper { get; }
        private readonly IAppUserService _appUserService;
        private readonly ISessionService _sessionService;
        private readonly IHeroService _heroService;
        private readonly IBlockService _blockService;

        public CoreGameController(
            ILogger<CoreGameController> logger,
            IMapper mapper, 
            IAppUserService appUserService,
            ISessionService sessionService,
            IHeroService heroService,
            IBlockService blockService)
        {
            _blockService = blockService;
            _logger = logger;
            _mapper = mapper;
            _appUserService = appUserService;
            _sessionService = sessionService;
            _heroService = heroService;
        }

        [Authorize]
        [HttpPost("pickhero")]
        public async Task<ActionResult> PickHeroAsync(PickHeroDto pickHeroDto)
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());
            var result = _heroService.CreateHero(user, pickHeroDto.HeroType);
            return Ok();
        }

        [Authorize]
        [HttpPost("loadgame")]
        public async Task<ActionResult> LoadSessionAsync(DeckWindowDto deckWindowDto)
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());

            if(user.SessionId == null || user.SessionId == 0)
            {
                return BadRequest("User has no active session");
            }

            var result = await _sessionService.LoadSessionAsync(user.SessionId?? default(int), deckWindowDto.startX, deckWindowDto.startY, deckWindowDto.endX, deckWindowDto.endY);

            var dto = _mapper.Map<GameSessionDto>(result.Data);
            dto.BlocksShape = _mapper.Map<ICollection<ICollection<GameBlockDto>>>(result.Data.ConstructTwoDimensionalBoard());
            dto.Blocks = null;
            return Ok(dto);
        }

        [Authorize]
        [HttpGet("initializecurrentturn")]
        public async Task<ActionResult> InitializeCurrentTurn(int sessionId)
        {
            var activePlayerModel = await _sessionService.GetActivePlayer(sessionId);
            var dto = _mapper.Map<ActivePlayerDto>(activePlayerModel);
            return Ok(dto);
        }

        [Authorize]
        [HttpGet("getheroinfo")]
        public async Task<ActionResult> GetHeroInformation()
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());
            var result = await _heroService.GetHeroInformationOfUser(user.Id);
            if(result.Succeeded)
            {
                var heroDto = _mapper.Map<GameHeroDto>(result.Data);
                return Ok(heroDto);
            }
            return BadRequest("Getting hero information failed");
        }

        [Authorize]
        [HttpPost("upgradeatribute")]
        public async Task<ActionResult> UpgradeAtribute([FromBody] HeroAttribute attribute)
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());
            var result = await _heroService.UpgradeAttributeOfUserHero(user.Id, attribute);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        
        }


        [Authorize]
        [HttpPost("resolveconflict")]
        public async Task<ActionResult> ResolveConflict([FromBody] int blockId)
        {
            var attackerUser = await _appUserService.GetAppUser(User.GetUserName());
            var result = await _blockService.ResolveConflictOnBlock(blockId, attackerUser.Id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest("Battle result failed");
        }
    }
}
