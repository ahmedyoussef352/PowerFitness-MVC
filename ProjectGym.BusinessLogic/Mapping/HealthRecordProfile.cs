using AutoMapper;
using ProjectGym.BusinessLogic.ViewModel.HeathRecord;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.BusinessLogic.Mapping
{
    public class HealthRecordProfile : Profile
    {
        public HealthRecordProfile()
        {
            CreateMap<HealthRecordViewModel, HealthRecord>();
            CreateMap<HealthRecord, HealthRecordViewModel>();
        }
    }
}
