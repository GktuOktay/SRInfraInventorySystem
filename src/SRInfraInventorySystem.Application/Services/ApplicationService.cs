using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using AppEntity = SRInfraInventorySystem.Core.Entities.Application;
using SRInfraInventorySystem.Core.Enums;
using SRInfraInventorySystem.Repository.Interfaces;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<ApplicationDto>>> GetAllAsync()
        {
            try
            {
                var applications = await _applicationRepository.GetApplicationsWithDetailsAsync();
                var applicationDtos = _mapper.Map<IEnumerable<ApplicationDto>>(applications);
                return ApiResult<IEnumerable<ApplicationDto>>.SuccessResult(applicationDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ApplicationDto>>.ErrorResult($"Uygulamalar getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ApplicationDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var application = await _applicationRepository.GetApplicationWithDetailsByIdAsync(id);
                if (application == null)
                    return ApiResult<ApplicationDto>.ErrorResult("Uygulama bulunamadı");

                var applicationDto = _mapper.Map<ApplicationDto>(application);
                return ApiResult<ApplicationDto>.SuccessResult(applicationDto);
            }
            catch (Exception ex)
            {
                return ApiResult<ApplicationDto>.ErrorResult($"Uygulama getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ApplicationDto>> CreateAsync(CreateApplicationDto createApplicationDto)
        {
            try
            {
                var application = _mapper.Map<AppEntity>(createApplicationDto);
                var createdApplication = await _applicationRepository.AddAsync(application);
                var applicationDto = _mapper.Map<ApplicationDto>(createdApplication);
                return ApiResult<ApplicationDto>.SuccessResult(applicationDto, "Uygulama başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<ApplicationDto>.ErrorResult($"Uygulama oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ApplicationDto>> UpdateAsync(Guid id, CreateApplicationDto updateApplicationDto)
        {
            try
            {
                var existingApplication = await _applicationRepository.GetByIdAsync(id);
                if (existingApplication == null)
                    return ApiResult<ApplicationDto>.ErrorResult("Uygulama bulunamadı");

                _mapper.Map(updateApplicationDto, existingApplication);
                var updatedApplication = await _applicationRepository.UpdateAsync(existingApplication);
                var applicationDto = _mapper.Map<ApplicationDto>(updatedApplication);
                return ApiResult<ApplicationDto>.SuccessResult(applicationDto, "Uygulama başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<ApplicationDto>.ErrorResult($"Uygulama güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var application = await _applicationRepository.GetByIdAsync(id);
                if (application == null)
                    return ApiResult<bool>.ErrorResult("Uygulama bulunamadı");

                // Bağlı veri kontrolü
                var hasRelatedData = await _applicationRepository.HasRelatedDataAsync(id);
                if (hasRelatedData)
                    return ApiResult<bool>.ErrorResult("Bu uygulama silinemez. Bağlı veritabanları veya erişim logları bulunmaktadır.");

                // Uygulamayı soft delete yap
                await _applicationRepository.DeleteAsync(application);
                return ApiResult<bool>.SuccessResult(true, "Uygulama başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Uygulama silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<ApplicationDto>>> GetByServerAsync(Guid serverId)
        {
            try
            {
                var applications = await _applicationRepository.GetApplicationsByServerAsync(serverId);
                var applicationDtos = _mapper.Map<IEnumerable<ApplicationDto>>(applications);
                return ApiResult<IEnumerable<ApplicationDto>>.SuccessResult(applicationDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ApplicationDto>>.ErrorResult($"Uygulamalar getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<ApplicationDto>>> GetByStatusAsync(ApplicationStatus status)
        {
            try
            {
                var applications = await _applicationRepository.GetApplicationsByStatusAsync(status);
                var applicationDtos = _mapper.Map<IEnumerable<ApplicationDto>>(applications);
                return ApiResult<IEnumerable<ApplicationDto>>.SuccessResult(applicationDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ApplicationDto>>.ErrorResult($"Uygulamalar getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
} 