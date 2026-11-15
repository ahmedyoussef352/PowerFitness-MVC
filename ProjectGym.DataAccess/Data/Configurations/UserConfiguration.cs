using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public virtual void Configure(EntityTypeBuilder<User> builder)
    {



        //Length constraints for string properties
        builder.Property(u => u.Name)
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasMaxLength(100);

        builder.Property(u => u.Phone)
                .HasMaxLength(15);

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .HasMaxLength(100);

            address.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(50);

            address.Property(a => a.BuildingNumber)
                .HasColumnName("BuildingNumber")
                .HasMaxLength(50);
        });

        // Unique constraints for Email and Phone
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.Phone)
            .IsUnique();

        // Check phone 11 And like 01[0125]

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_User_Phone", "Phone LIKE '01[0125]%' AND LEN(Phone) = 11");
        });

        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}
