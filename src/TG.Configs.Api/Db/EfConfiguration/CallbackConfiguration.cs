using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TG.Configs.Api.Entities;

namespace TG.Configs.Api.Db.EfConfiguration
{
    public class CallbackConfiguration : IEntityTypeConfiguration<Callback>
    {
        public void Configure(EntityTypeBuilder<Callback> entity)
        {
            entity.HasKey(c => c.Id);
            entity.HasOne<Entities.Config>()
                .WithMany(c => c.Callbacks)
                .HasForeignKey(c => c.ConfigId);
        }
    }
}