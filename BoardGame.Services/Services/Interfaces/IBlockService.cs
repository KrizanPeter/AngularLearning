using System.Collections.Generic;
using System.Threading.Tasks;

using BoardGame.Domain.Models;
using BoardGame.Services.ReturnStates;

namespace BoardGame.Services.Services.Interfaces
{
    public interface IBlockService
    {
        Task<OperationalResult<BlockModel>> GetBlockById(int id);
        //Task<OperationalResult<List<BlockModel>>> MoveHeroToBlock(int userId, int targetBlockId);
    }
}
