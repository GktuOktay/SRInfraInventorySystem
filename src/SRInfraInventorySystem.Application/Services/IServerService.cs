using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Enums;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IServerService
    {
        Task<ApiResult<IEnumerable<ServerDto>>> GetAllAsync();
        Task<ApiResult<ServerDto>> GetByIdAsync(Guid id);
        Task<ApiResult<ServerDto>> CreateAsync(CreateServerDto createServerDto);
        Task<ApiResult<ServerDto>> UpdateAsync(Guid id, CreateServerDto updateServerDto);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<ServerDto>>> GetByStatusAsync(ServerStatus status);
        Task<ApiResult<IEnumerable<ServerDto>>> GetByOperatingSystemAsync(string operatingSystem);
    }
} 