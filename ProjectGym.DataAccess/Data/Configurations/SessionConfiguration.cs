using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "Ck_Session_StartDate_Capacity",
                    "[Capacity] BETWEEN 1 AND 25 "
                    );
                t.HasCheckConstraint(
                                "Ck_Session_Start_End",
                                "[EndTime] > [StartTime]"
                            );
            });

            builder.HasQueryFilter(s => !s.IsDeleted);

        }
    }
}
