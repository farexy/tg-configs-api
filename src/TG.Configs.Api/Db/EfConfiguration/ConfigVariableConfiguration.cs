using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TG.Configs.Api.Entities;

namespace TG.Configs.Api.Db.EfConfiguration
{
    public class ConfigVariableConfiguration : IEntityTypeConfiguration<ConfigVariable>
    {
        public void Configure(EntityTypeBuilder<ConfigVariable> entity)
        {
            entity.HasKey(v => new {v.ConfigId, v.Key});
        }
    }
}