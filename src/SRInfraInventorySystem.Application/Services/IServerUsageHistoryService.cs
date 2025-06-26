using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IServerUsageHistoryService
    {
        Task<ApiResult<IEnumerable<ServerUsageHistoryDto>>> GetAllAsync();
        Task<ApiResult<ServerUsageHistoryDto>> GetByIdAsync(Guid id);
        Task<ApiResult<ServerUsageHistoryDto>> CreateAsync(CreateServerUsageHistoryDto createDto);
        Task<ApiResult<ServerUsageHistoryDto>> UpdateAsync(Guid id, CreateServerUsageHistoryDto updateDto);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<ServerUsageHistoryDto>>> GetHistoryByServerIdAsync(Guid serverId);
        Task<ApiResult<IEnumerable<ServerUsageHistoryDto>>> GetHistoryByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ApiResult<ServerUsageHistoryDto>> GetLatestUsageByServerIdAsync(Guid serverId);
        Task<ApiResult<double>> GetAverageCpuUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate);
        Task<ApiResult<double>> GetAverageMemoryUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate);
    }
} 