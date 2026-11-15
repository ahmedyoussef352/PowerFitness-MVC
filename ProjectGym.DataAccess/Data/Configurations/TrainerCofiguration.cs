using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Configurations;

public class TrainerCofiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        //enum to String in db
        builder.Property(t => t.Speciality)
            .HasConversion<string>()
            .HasMaxLength(50);

    }
}

