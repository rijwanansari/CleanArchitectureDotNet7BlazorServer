using Domain.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Master
{
    public class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
    {
        public void Configure(EntityTypeBuilder<AppSetting> builder)
        {
            builder
                .ToTable(nameof(AppSetting));
            builder.Property(nameof(AppSetting.ReferenceKey)).HasMaxLength(100).IsRequired();
            builder.Property(nameof(AppSetting.Value)).HasMaxLength(500);
            builder.Property(nameof(AppSetting.Description)).HasMaxLength(500).IsRequired();
            builder.Property(nameof(AppSetting.Type)).HasMaxLength(50).IsRequired();
        }
    }
}
