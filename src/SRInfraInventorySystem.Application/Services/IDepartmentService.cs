using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public interface IDepartmentService
    {
        Task<ApiResult<IEnumerable<DepartmentDto>>> GetAllAsync();
        Task<ApiResult<DepartmentDto>> GetByIdAsync(Guid id);
        Task<ApiResult<DepartmentDto>> CreateAsync(CreateDepartmentDto createDepartmentDto);
        Task<ApiResult<DepartmentResponseDto>> CreateSimpleAsync(CreateDepartmentDto createDepartmentDto);
        Task<ApiResult<DepartmentDto>> UpdateAsync(Guid id, UpdateDepartmentDto updateDepartmentDto);
        /// <summary>
        /// Departmanı ve tüm alt departmanlarını siler (Cascade Delete with Transaction)
        /// </summary>
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<IEnumerable<DepartmentDto>>> GetRootDepartmentsAsync();
        Task<ApiResult<IEnumerable<DepartmentDropdownDto>>> GetRootDepartmentsForSelectListAsync();
        Task<ApiResult<IEnumerable<DepartmentDto>>> GetSubDepartmentsAsync(Guid parentId);
        Task<ApiResult<PagedResult<DepartmentDto>>> GetFilteredDepartmentsAsync(
            string? name = null, 
            int pageNumber = 1, 
            int pageSize = 10);
        Task<ApiResult<DepartmentDto>> AssignManagerAsync(AssignManagerDto assignManagerDto);
        /// <summary>
        /// Departmanları hiyerarşik ve alfabetik olarak döner.
        /// </summary>
        Task<ApiResult<IEnumerable<HierarchicalDepartmentDto>>> GetAllHierarchicalAsync();
    }
} 