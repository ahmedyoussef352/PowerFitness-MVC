using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasOne(b => b.Member)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(b => b.IsAttended)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(b => new
        {
            b.SessionId,
            b.MemberId
        })
            .IsUnique()
            .HasFilter("IsDeleted = 0");

        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}
