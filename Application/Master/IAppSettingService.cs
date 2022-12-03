using Application.Common.Model;
using Application.Master.Dto;
using Domain.Master;

namespace Application.Master
{
    public interface IAppSettingService
    {
        Task<ResponseModel> GetAppSettingsAsync();
        Task<ResponseModel> GetAppSettingByIdAsync(int Id);
        Task<ResponseModel> UpsertAsync(AppSettingVm appSetting);
        Task<ResponseModel> DeleteAsync(int Id);
    }
}
