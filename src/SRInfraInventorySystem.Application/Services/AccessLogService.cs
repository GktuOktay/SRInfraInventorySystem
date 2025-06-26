using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Repository.Interfaces;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public class AccessLogService : IAccessLogService
    {
        private readonly IAccessLogRepository _accessLogRepository;
        private readonly IMapper _mapper;

        public AccessLogService(IAccessLogRepository accessLogRepository, IMapper mapper)
        {
            _accessLogRepository = accessLogRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetAllAsync()
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetAccessLogsWithDetailsAsync();
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<AccessLogDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var accessLog = await _accessLogRepository.GetByIdAsync(id);
                if (accessLog == null)
                    return ApiResult<AccessLogDto>.ErrorResult("Erişim logu bulunamadı");

                var accessLogDto = _mapper.Map<AccessLogDto>(accessLog);
                return ApiResult<AccessLogDto>.SuccessResult(accessLogDto);
            }
            catch (Exception ex)
            {
                return ApiResult<AccessLogDto>.ErrorResult($"Erişim logu getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<AccessLogDto>> CreateAsync(CreateAccessLogDto createDto)
        {
            try
            {
                var accessLog = _mapper.Map<AccessLog>(createDto);
                var createdAccessLog = await _accessLogRepository.AddAsync(accessLog);
                var accessLogDto = _mapper.Map<AccessLogDto>(createdAccessLog);
                return ApiResult<AccessLogDto>.SuccessResult(accessLogDto, "Erişim logu başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<AccessLogDto>.ErrorResult($"Erişim logu oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<AccessLogDto>> UpdateAsync(Guid id, CreateAccessLogDto updateDto)
        {
            try
            {
                var existingAccessLog = await _accessLogRepository.GetByIdAsync(id);
                if (existingAccessLog == null)
                    return ApiResult<AccessLogDto>.ErrorResult("Erişim logu bulunamadı");

                _mapper.Map(updateDto, existingAccessLog);
                var updatedAccessLog = await _accessLogRepository.UpdateAsync(existingAccessLog);
                var accessLogDto = _mapper.Map<AccessLogDto>(updatedAccessLog);
                return ApiResult<AccessLogDto>.SuccessResult(accessLogDto, "Erişim logu başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<AccessLogDto>.ErrorResult($"Erişim logu güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var accessLog = await _accessLogRepository.GetByIdAsync(id);
                if (accessLog == null)
                    return ApiResult<bool>.ErrorResult("Erişim logu bulunamadı");

                await _accessLogRepository.DeleteAsync(accessLog);
                return ApiResult<bool>.SuccessResult(true, "Erişim logu başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Erişim logu silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByServerIdAsync(Guid serverId)
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetAccessLogsByServerIdAsync(serverId);
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Sunucu erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByApplicationIdAsync(Guid applicationId)
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetAccessLogsByApplicationIdAsync(applicationId);
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Uygulama erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByDatabaseIdAsync(Guid databaseId)
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetAccessLogsByDatabaseIdAsync(databaseId);
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Veritabanı erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByUserNameAsync(string userName)
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetAccessLogsByUserNameAsync(userName);
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Kullanıcı erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetAccessLogsByDateRangeAsync(startDate, endDate);
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Tarih aralığına göre erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AccessLogDto>>> GetActiveAccessLogsAsync()
        {
            try
            {
                var accessLogs = await _accessLogRepository.GetActiveAccessLogsAsync();
                var accessLogDtos = _mapper.Map<IEnumerable<AccessLogDto>>(accessLogs);
                return ApiResult<IEnumerable<AccessLogDto>>.SuccessResult(accessLogDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AccessLogDto>>.ErrorResult($"Aktif erişim logları getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
} 