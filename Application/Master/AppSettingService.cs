using Application.Common.Error;
using Application.Common.Interface;
using Application.Common.Model;
using Application.Master.Dto;
using AutoMapper;
using Domain.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Master
{
    internal class AppSettingService : IAppSettingService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppSettingService> logger;
        private readonly IMapper mapper;
        private readonly IErrorMessageLog errorMessageLog;
        #endregion
        #region Ctor
        public AppSettingService(IUnitOfWork unitOfWork, ILogger<AppSettingService> logger, IMapper mapper, IErrorMessageLog errorMessageLog)
        {
            _unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.errorMessageLog = errorMessageLog;
        }
        #endregion

        #region AppSetting

        #region Command
        public async Task<ResponseModel> UpsertAsync(AppSettingVm appSettingVm)
        {
            var appSetting = mapper.Map<AppSetting>(appSettingVm);
            try
            {
                if (appSetting.Id > 0)
                    _unitOfWork.Repository<AppSetting>().Update(appSetting);
                else
                  await _unitOfWork.Repository<AppSetting>().AddAsync(appSetting);

                await _unitOfWork.SaveAsync();
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, mapper.Map<AppSettingVm>(appSetting));
            }
            catch (Exception ex)
            {
                Log(nameof(UpsertAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }

        }
        public async Task<ResponseModel> DeleteAsync(int Id)
        {
            try
            {
                var appSetting = await _unitOfWork.Repository<AppSetting>().Get(Id);
                if (appSetting != null)
                {
                    appSetting.IsDeleted = true;
                    await _unitOfWork.SaveAsync();
                    return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, null);
                }
                else
                    return ResponseModel.FailureResponse("Not Found");
            }
            catch (Exception ex)
            {
                Log(nameof(DeleteAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }
        }
        #endregion

        #region Queries
        public async Task<ResponseModel> GetAppSettingsAsync()
        {
            try
            {
                var appsettigs = await _unitOfWork.Repository<AppSetting>()
               .TableNoTracking
               .OrderBy(t => t.ReferenceKey)
               .ToListAsync();
                var appSettingsVm = mapper.Map<List<AppSettingVm>>(appsettigs);
                return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, appSettingsVm);
            }
            catch (Exception ex)
            {
                Log(nameof(GetAppSettingsAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }
           
        }
        public async Task<ResponseModel> GetAppSettingByIdAsync(int Id)
        {
            try
            {
                var appSetting = await _unitOfWork.Repository<AppSetting>().Get(Id);
                if (appSetting != null)
                {
                    var appSettingVm = mapper.Map<AppSettingVm>(appSetting);
                    return ResponseModel.SuccessResponse(GlobalDeclaration._successResponse, appSettingVm);
                }
                else
                    return ResponseModel.FailureResponse("Not Found");
            }
            catch (Exception ex)
            {
                Log(nameof(DeleteAsync), ex.Message);
                logger?.LogError(ex.ToString());
                return ResponseModel.FailureResponse(GlobalDeclaration._internalServerError);
            }
        }

        #endregion

        #endregion

        #region Error
        private void Log(string method, string msg)
        {
            errorMessageLog.LogError("Application", "AppSettingService", method, msg);
        }
        #endregion
    }
}
