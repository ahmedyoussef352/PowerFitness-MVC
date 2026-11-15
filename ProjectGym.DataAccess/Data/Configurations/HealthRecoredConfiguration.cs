using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Configurations
{
    public class HealthRecoredConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(hr => hr.Height)
                .HasPrecision(5, 2);
            builder.Property(hr => hr.Weight)
                .HasPrecision(5, 2);

            builder.Property(hr => hr.BloodType)
                .HasConversion<string>()
                .HasMaxLength(100);

            builder.HasOne(hr => hr.Member)
                   .WithOne(m => m.HealthRecord)
                   .HasForeignKey<HealthRecord>(hr => hr.MemberId);

            builder.HasQueryFilter(hr => !hr.IsDeleted);
        }
    }
}
