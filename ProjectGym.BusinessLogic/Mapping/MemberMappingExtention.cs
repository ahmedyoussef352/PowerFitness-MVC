using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.BusinessLogic.Mapping
{
    public static class MemberMappingExtention
    {
        public static EditMemberViewModel ToEditViewModel(this Member member)
        {
            return new()
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City,
                Photo = member.ImageUrl,
            };
        }

        public static MemberIndexViewModel ToGetMembers(this Member member)
        {
            return new()
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                JoinDate = member.JoinDate
            };
        }
    }
}
