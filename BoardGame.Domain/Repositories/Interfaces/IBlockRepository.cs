using BoardGame.Domain.Entities;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IBlockRepository : IRepository<Block>
    {
        Block GetCenterBlock(int sessionId, int centerBlockPosition);
        Block GetBlockWithHeroes(int blockId);
    }
}
