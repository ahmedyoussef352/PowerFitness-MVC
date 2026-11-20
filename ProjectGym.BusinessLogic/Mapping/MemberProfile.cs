using AutoMapper;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Entities.ValueObjects;

namespace ProjectGym.BusinessLogic.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberIndexViewModel>()
                .ForMember(d => d.PhotoUrl,
                o => o.MapFrom(s => s.ImageUrl))
                .ForMember(d => d.JoinDate,
                o => o.MapFrom(s => DateOnly.FromDateTime(DateTime.UtcNow)))
               .ForMember(d => d.Gender,
               o => o.MapFrom(s => s.Gender.ToString())
                );

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(c => c.Address,
                o => o.MapFrom(s => new Address
                {
                    BuildingNumber = s.BuildingNumber,
                    City = s.City,
                    Street = s.Street
                }));

            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(d => d.PhotoUrl,
                o => o.MapFrom(s => s.ImageUrl))
                .ForMember(d => d.Gender,
               o => o.MapFrom(s => s.Gender.ToString())
                )
                  .ForMember(d => d.DateOfBirth,
               o => o.MapFrom(s => s.DateOfBirth.ToShortDateString())
                )
                  .ForMember(d => d.Address,
                o => o.MapFrom(
                    s => $"{s.Address.BuildingNumber} - {s.Address.Street} - {s.Address.City}"
                ))
            .ForMember(d => d.PlanName,
                o => o.MapFrom(
                    s => ResolvePlanName(s.MemberShips.FirstOrDefault())
                ))
             .ForMember(d => d.MembershipStartDate,
                o => o.MapFrom(
                    s => ResolveMembershipStartDate(s.MemberShips.FirstOrDefault())
                ))
             .ForMember(d => d.MembershipEndDate,
                o => o.MapFrom(
                    s => ResolveMembershipEndDate(s.MemberShips.FirstOrDefault())
                ));

            CreateMap<MemberDetailsViewModel, MemberDeleteViewModel>();


            CreateMap<Member, EditMemberViewModel>()
                .ForMember(d => d.Photo,
                o => o.MapFrom(s => s.ImageUrl))
                .ForMember(d => d.BuildingNumber,
                o => o.MapFrom(
                    s => s.Address.BuildingNumber
                ))
                .ForMember(d => d.Street,
                o => o.MapFrom(
                    s => s.Address.Street
                ))

                .ForMember(d => d.City,
                o => o.MapFrom(
                    s => s.Address.City
                ));

            CreateMap<EditMemberViewModel, Member>()
                .ForMember(d => d.Name,
                o => o.Ignore())
                .ForMember(d => d.ImageUrl,
                o => o.Ignore());

        }

        private string ResolveMembershipStartDate(MemberShip? memberShip)
        => memberShip?.StartDate.ToShortDateString() ?? "-";
        private string ResolveMembershipEndDate(MemberShip? memberShip)
       => memberShip?.StartDate.ToShortDateString() ?? "-";

        private string ResolvePlanName(MemberShip? memberShip)
        => memberShip?.Plan.Name ?? "No Active Plan";



    }
}
