using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Configurations;

public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
{
    public void Configure(EntityTypeBuilder<MemberShip> builder)
    {
        builder.HasCheckConstraint(
            "Ck_MemberShip_StartDate_EndDate",
            "[EndDate] > [StartDate]"
            );

        //builder.HasIndex(ms => new { ms.MemberId, ms.PlanId })
        //    .IsUnique();


        builder.HasQueryFilter(ms => !ms.IsDeleted);
    }
}
