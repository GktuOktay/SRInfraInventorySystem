using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IDatabaseService
    {
        Task<ApiResult<IEnumerable<DatabaseDto>>> GetAllAsync();
        Task<ApiResult<DatabaseDto>> GetByIdAsync(Guid id);
        Task<ApiResult<DatabaseDto>> CreateAsync(CreateDatabaseDto createDatabaseDto);
        Task<ApiResult<DatabaseDto>> UpdateAsync(Guid id, CreateDatabaseDto updateDatabaseDto);
        Task<ApiResult<DatabaseDto>> UpdatePartialAsync(Guid id, UpdateDatabaseDto updateDatabaseDto);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<DatabaseDto>>> GetByServerAsync(Guid serverId);
        Task<ApiResult<IEnumerable<DatabaseDto>>> GetByTypeAsync(string type);
    }
} 