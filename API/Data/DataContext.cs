using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>,
        AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

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