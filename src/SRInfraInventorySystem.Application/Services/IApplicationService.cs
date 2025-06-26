using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Enums;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IApplicationService
    {
        Task<ApiResult<IEnumerable<ApplicationDto>>> GetAllAsync();
        Task<ApiResult<ApplicationDto>> GetByIdAsync(Guid id);
        Task<ApiResult<ApplicationDto>> CreateAsync(CreateApplicationDto createApplicationDto);
        Task<ApiResult<ApplicationDto>> UpdateAsync(Guid id, CreateApplicationDto updateApplicationDto);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<ApplicationDto>>> GetByServerAsync(Guid serverId);
        Task<ApiResult<IEnumerable<ApplicationDto>>> GetByStatusAsync(ApplicationStatus status);
    }
} 