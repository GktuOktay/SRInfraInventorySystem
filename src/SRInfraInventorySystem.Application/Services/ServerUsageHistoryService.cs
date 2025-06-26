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
    public class ServerUsageHistoryService : IServerUsageHistoryService
    {
        private readonly IServerUsageHistoryRepository _serverUsageHistoryRepository;
        private readonly IMapper _mapper;

        public ServerUsageHistoryService(IServerUsageHistoryRepository serverUsageHistoryRepository, IMapper mapper)
        {
            _serverUsageHistoryRepository = serverUsageHistoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<ServerUsageHistoryDto>>> GetAllAsync()
        {
            try
            {
                var usageHistories = await _serverUsageHistoryRepository.GetUsageHistoryWithServerDetailsAsync();
                var usageHistoryDtos = _mapper.Map<IEnumerable<ServerUsageHistoryDto>>(usageHistories);
                return ApiResult<IEnumerable<ServerUsageHistoryDto>>.SuccessResult(usageHistoryDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ServerUsageHistoryDto>>.ErrorResult($"Sunucu kullanım geçmişi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerUsageHistoryDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var usageHistory = await _serverUsageHistoryRepository.GetByIdAsync(id);
                if (usageHistory == null)
                    return ApiResult<ServerUsageHistoryDto>.ErrorResult("Kullanım geçmişi bulunamadı");

                var usageHistoryDto = _mapper.Map<ServerUsageHistoryDto>(usageHistory);
                return ApiResult<ServerUsageHistoryDto>.SuccessResult(usageHistoryDto);
            }
            catch (Exception ex)
            {
                return ApiResult<ServerUsageHistoryDto>.ErrorResult($"Kullanım geçmişi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerUsageHistoryDto>> CreateAsync(CreateServerUsageHistoryDto createDto)
        {
            try
            {
                var usageHistory = _mapper.Map<ServerUsageHistory>(createDto);
                var createdUsageHistory = await _serverUsageHistoryRepository.AddAsync(usageHistory);
                var usageHistoryDto = _mapper.Map<ServerUsageHistoryDto>(createdUsageHistory);
                return ApiResult<ServerUsageHistoryDto>.SuccessResult(usageHistoryDto, "Kullanım geçmişi başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<ServerUsageHistoryDto>.ErrorResult($"Kullanım geçmişi oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerUsageHistoryDto>> UpdateAsync(Guid id, CreateServerUsageHistoryDto updateDto)
        {
            try
            {
                var existingUsageHistory = await _serverUsageHistoryRepository.GetByIdAsync(id);
                if (existingUsageHistory == null)
                    return ApiResult<ServerUsageHistoryDto>.ErrorResult("Kullanım geçmişi bulunamadı");

                _mapper.Map(updateDto, existingUsageHistory);
                var updatedUsageHistory = await _serverUsageHistoryRepository.UpdateAsync(existingUsageHistory);
                var usageHistoryDto = _mapper.Map<ServerUsageHistoryDto>(updatedUsageHistory);
                return ApiResult<ServerUsageHistoryDto>.SuccessResult(usageHistoryDto, "Kullanım geçmişi başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<ServerUsageHistoryDto>.ErrorResult($"Kullanım geçmişi güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var usageHistory = await _serverUsageHistoryRepository.GetByIdAsync(id);
                if (usageHistory == null)
                    return ApiResult<bool>.ErrorResult("Kullanım geçmişi bulunamadı");

                await _serverUsageHistoryRepository.DeleteAsync(usageHistory);
                return ApiResult<bool>.SuccessResult(true, "Kullanım geçmişi başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Kullanım geçmişi silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<ServerUsageHistoryDto>>> GetHistoryByServerIdAsync(Guid serverId)
        {
            try
            {
                var usageHistories = await _serverUsageHistoryRepository.GetHistoryByServerIdAsync(serverId);
                var usageHistoryDtos = _mapper.Map<IEnumerable<ServerUsageHistoryDto>>(usageHistories);
                return ApiResult<IEnumerable<ServerUsageHistoryDto>>.SuccessResult(usageHistoryDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ServerUsageHistoryDto>>.ErrorResult($"Sunucu kullanım geçmişi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<ServerUsageHistoryDto>>> GetHistoryByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var usageHistories = await _serverUsageHistoryRepository.GetHistoryByDateRangeAsync(startDate, endDate);
                var usageHistoryDtos = _mapper.Map<IEnumerable<ServerUsageHistoryDto>>(usageHistories);
                return ApiResult<IEnumerable<ServerUsageHistoryDto>>.SuccessResult(usageHistoryDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ServerUsageHistoryDto>>.ErrorResult($"Tarih aralığına göre kullanım geçmişi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerUsageHistoryDto>> GetLatestUsageByServerIdAsync(Guid serverId)
        {
            try
            {
                var latestUsage = await _serverUsageHistoryRepository.GetLatestUsageByServerIdAsync(serverId);
                if (latestUsage == null)
                    return ApiResult<ServerUsageHistoryDto>.ErrorResult("Son kullanım geçmişi bulunamadı");

                var usageHistoryDto = _mapper.Map<ServerUsageHistoryDto>(latestUsage);
                return ApiResult<ServerUsageHistoryDto>.SuccessResult(usageHistoryDto);
            }
            catch (Exception ex)
            {
                return ApiResult<ServerUsageHistoryDto>.ErrorResult($"Son kullanım geçmişi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<double>> GetAverageCpuUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var avgCpuUsage = await _serverUsageHistoryRepository.GetAverageCpuUsageByServerIdAsync(serverId, startDate, endDate);
                return ApiResult<double>.SuccessResult(avgCpuUsage);
            }
            catch (Exception ex)
            {
                return ApiResult<double>.ErrorResult($"Ortalama CPU kullanımı hesaplanırken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<double>> GetAverageMemoryUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var avgMemoryUsage = await _serverUsageHistoryRepository.GetAverageMemoryUsageByServerIdAsync(serverId, startDate, endDate);
                return ApiResult<double>.SuccessResult(avgMemoryUsage);
            }
            catch (Exception ex)
            {
                return ApiResult<double>.ErrorResult($"Ortalama hafıza kullanımı hesaplanırken hata oluştu: {ex.Message}");
            }
        }
    }
} 