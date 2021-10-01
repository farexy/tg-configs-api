using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Entities;
using TG.Core.Db.Postgres;

namespace TG.Configs.Api.Db
{
    public class ApplicationDbContext : TgDbContext
    {
        public DbSet<Entities.Config> Configs { get; set; } = default!;
        public DbSet<Callback> Callbacks { get; set; } = default!;
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}