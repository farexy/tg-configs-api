using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TG.Configs.Api.Db.EfConfiguration
{
    public class ConfigConfiguration : IEntityTypeConfiguration<Entities.Config>
    {
        public void Configure(EntityTypeBuilder<Entities.Config> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Content)
                .HasColumnType("jsonb");
        }
    }
}