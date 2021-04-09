using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Hero> Heroes {get; set;}
        public DbSet<HeroType> HeroTypes { get; set; }
        public DbSet<GameBlock> GameBlocks { get; set; }
        public DbSet<GameBlockHall> GameBlockHalls { get; set; }
        public DbSet<GameBlockRoom> GameBlockRooms { get; set; }
        public DbSet<GamePlan> GamePlans { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<MonsterType> MonsterTypes { get; set; }
        public DbSet<Session> Sessions { get; set; }


    }
}