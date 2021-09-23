using BoardGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IBlockRepository : IRepository<Block>
    {
        Block GetCenterBlock(int sessionId, int centerBlockPosition);
    }
}
