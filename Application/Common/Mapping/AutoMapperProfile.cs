using Application.Master.Dto;

namespace Application.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppSettingVm, AppSetting>().ReverseMap();
            CreateMap<ReferenceField, ReferenceFieldVm>().ReverseMap();
           
        }
    }
}
