using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IPersonnelService
    {
        Task<ApiResult<IEnumerable<PersonnelDto>>> GetAllAsync();
        Task<ApiResult<PersonnelDto>> GetByIdAsync(Guid id);
        Task<ApiResult<PersonnelDto>> CreateAsync(CreatePersonnelDto createPersonnelDto);
        Task<ApiResult<PersonnelDto>> UpdateAsync(Guid id, CreatePersonnelDto updatePersonnelDto);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<PersonnelDto>>> GetByDepartmentAsync(Guid departmentId);
        Task<ApiResult<IEnumerable<PersonnelDto>>> GetActivePersonnelAsync();
        Task<ApiResult<PersonnelDto>> GetByUserNameAsync(string userName);
    }
} 