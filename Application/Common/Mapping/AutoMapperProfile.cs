using Application.Master.Dto;
using AutoMapper;
using Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
