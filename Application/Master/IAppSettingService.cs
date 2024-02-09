using Application.Common.Interface;
using Application.Common.Model;
using Application.Master.Dto;
using Domain.Master;

namespace Application.Master
{
    public interface IAppSettingService : IRepositoryService<AppSettingVm>
    {
    }
}
