using Domain.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Master
{
    public class ReferenceFieldConfiguration : IEntityTypeConfiguration<ReferenceField>
    {
        public void Configure(EntityTypeBuilder<ReferenceField> builder)
        {
            builder
               .ToTable(nameof(ReferenceField));
            builder.Property(nameof(ReferenceField.Title)).IsRequired().HasMaxLength(200);
            builder.Property(nameof(ReferenceField.ReferenceType)).IsRequired().HasMaxLength(100);
        }
    }
}
