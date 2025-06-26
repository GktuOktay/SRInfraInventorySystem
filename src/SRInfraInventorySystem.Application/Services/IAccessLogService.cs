using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IAccessLogService
    {
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetAllAsync();
        Task<ApiResult<AccessLogDto>> GetByIdAsync(Guid id);
        Task<ApiResult<AccessLogDto>> CreateAsync(CreateAccessLogDto createDto);
        Task<ApiResult<AccessLogDto>> UpdateAsync(Guid id, CreateAccessLogDto updateDto);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByServerIdAsync(Guid serverId);
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByApplicationIdAsync(Guid applicationId);
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByDatabaseIdAsync(Guid databaseId);
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByUserNameAsync(string userName);
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetAccessLogsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ApiResult<IEnumerable<AccessLogDto>>> GetActiveAccessLogsAsync();
    }
} 