using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IBlockRepository : IRepository<Block>
    {
        Block GetCenterBlock(int sessionId, int centerBlockPosition);
        Block GetBlockWithHeroesAndMonster(int blockId);
        BlockModel GetBlockModelWithHeroesAndMonster(int blockId);
    }
}
