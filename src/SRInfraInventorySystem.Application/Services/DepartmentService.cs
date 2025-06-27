using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Repository.Interfaces;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        private int GetDepth(DepartmentDto dept, IEnumerable<DepartmentDto> all)
        {
            int depth = 0;
            var current = dept;
            while (current.ParentDepartmentId != null)
            {
                depth++;
                current = all.FirstOrDefault(d => d.Id == current.ParentDepartmentId);
                if (current == null) break;
            }
            return depth;
        }

        private List<DepartmentDto> HierarchicalSort(IEnumerable<DepartmentDto> departments)
        {
            var all = departments.ToList();
            var result = new List<DepartmentDto>();

            void AddChildren(Guid? parentId)
            {
                var children = all
                    .Where(d => d.ParentDepartmentId == parentId)
                    .OrderByDescending(d => GetDepth(d, all)) // önce derinlik büyükten küçüğe
                    .ThenBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase) // sonra alfabetik
                    .ToList();

                foreach (var child in children)
                {
                    result.Add(child);
                    AddChildren(child.Id);
                }
            }

            AddChildren(null);
            return result;
        }

        private List<DepartmentDto> FlattenDepartments(IEnumerable<DepartmentDto> departments, Guid? parentId = null)
        {
            var children = departments
                .Where(d => d.ParentDepartmentId == parentId)
                .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            var result = new List<DepartmentDto>();
            foreach (var child in children)
            {
                result.Add(child);
                result.AddRange(FlattenDepartments(departments, child.Id));
            }
            return result;
        }

        public async Task<ApiResult<IEnumerable<DepartmentDto>>> GetAllAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetDepartmentsWithDetailsAsync();
                var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
                var sorted = FlattenDepartments(departmentDtos);
                return ApiResult<IEnumerable<DepartmentDto>>.SuccessResult(sorted);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DepartmentDto>>.ErrorResult($"Müdürlükler getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<HierarchicalDepartmentDto>>> GetAllHierarchicalAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetDepartmentsWithDetailsAsync();
                var departmentDtos = _mapper.Map<List<HierarchicalDepartmentDto>>(departments);
                var hierarchy = BuildHierarchy(departmentDtos, null);
                return ApiResult<IEnumerable<HierarchicalDepartmentDto>>.SuccessResult(hierarchy);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<HierarchicalDepartmentDto>>.ErrorResult($"Departmanlar getirilirken hata oluştu: {ex.Message}");
            }
        }

        private List<HierarchicalDepartmentDto> BuildHierarchy(List<HierarchicalDepartmentDto> all, Guid? parentId)
        {
            return all
                .Where(d => d.ParentDepartmentId == parentId)
                .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
                .Select(d =>
                {
                    d.SubDepartments = BuildHierarchy(all, d.Id);
                    return d;
                })
                .ToList();
        }

        public async Task<ApiResult<DepartmentDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var department = await _departmentRepository.GetDepartmentWithDetailsByIdAsync(id);
                if (department == null)
                    return ApiResult<DepartmentDto>.ErrorResult("Müdürlük bulunamadı");

                var departmentDto = _mapper.Map<DepartmentDto>(department);
                return ApiResult<DepartmentDto>.SuccessResult(departmentDto);
            }
            catch (Exception ex)
            {
                return ApiResult<DepartmentDto>.ErrorResult($"Müdürlük getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DepartmentDto>> CreateAsync(CreateDepartmentDto createDepartmentDto)
        {
            try
            {
                var department = _mapper.Map<Department>(createDepartmentDto);
                var createdDepartment = await _departmentRepository.AddAsync(department);
                var departmentDto = _mapper.Map<DepartmentDto>(createdDepartment);
                return ApiResult<DepartmentDto>.SuccessResult(departmentDto, "Müdürlük başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<DepartmentDto>.ErrorResult($"Müdürlük oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DepartmentResponseDto>> CreateSimpleAsync(CreateDepartmentDto createDepartmentDto)
        {
            try
            {
                var department = _mapper.Map<Department>(createDepartmentDto);
                var createdDepartment = await _departmentRepository.AddAsync(department);
                var departmentResponseDto = _mapper.Map<DepartmentResponseDto>(createdDepartment);
                return ApiResult<DepartmentResponseDto>.SuccessResult(departmentResponseDto, "Departman başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<DepartmentResponseDto>.ErrorResult($"Departman oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DepartmentDto>> UpdateAsync(Guid id, UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                var existingDepartment = await _departmentRepository.GetByIdAsync(id);
                if (existingDepartment == null)
                    return ApiResult<DepartmentDto>.ErrorResult("Departman bulunamadı");

                // Sadece değişen alanları güncelle
                existingDepartment.Name = updateDepartmentDto.Name;
                existingDepartment.Description = updateDepartmentDto.Description;
                existingDepartment.ParentDepartmentId = updateDepartmentDto.ParentDepartmentId;
                existingDepartment.ManagerPersonnelId = updateDepartmentDto.ManagerPersonnelId;
                existingDepartment.UpdatedAt = DateTime.Now;

                var updatedDepartment = await _departmentRepository.UpdateAsync(existingDepartment);
                var departmentDto = _mapper.Map<DepartmentDto>(updatedDepartment);
                return ApiResult<DepartmentDto>.SuccessResult(departmentDto, "Departman başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<DepartmentDto>.ErrorResult($"Departman güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var department = await _departmentRepository.GetByIdAsync(id);
                if (department == null)
                    return ApiResult<bool>.ErrorResult("Departman bulunamadı");

                // Bağlı veri kontrolü
                var hasRelatedData = await _departmentRepository.HasRelatedDataAsync(id);
                if (hasRelatedData)
                    return ApiResult<bool>.ErrorResult("Bu departman silinemez. Alt departmanları veya bağlı personelleri bulunmaktadır.");

                // Departmanı soft delete yap
                await _departmentRepository.DeleteAsync(department);

                return ApiResult<bool>.SuccessResult(true, "Departman başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Departman silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<DepartmentDto>>> GetRootDepartmentsAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetRootDepartmentsAsync();
                var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
                return ApiResult<IEnumerable<DepartmentDto>>.SuccessResult(departmentDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DepartmentDto>>.ErrorResult($"Müdürlükler getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<DepartmentDropdownDto>>> GetRootDepartmentsForSelectListAsync()
        {
            try
            {
                // Tüm departmanları hiyerarşik yapıda getir
                var allDepartments = await _departmentRepository.GetDepartmentsWithDetailsAsync();
                var departmentDtos = _mapper.Map<List<DepartmentDropdownDto>>(allDepartments);
                
                // Hiyerarşik yapıyı oluştur
                var hierarchicalDepartments = BuildHierarchicalDropdown(departmentDtos, null);
                
                return ApiResult<IEnumerable<DepartmentDropdownDto>>.SuccessResult(hierarchicalDepartments);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DepartmentDropdownDto>>.ErrorResult($"Ana departmanlar getirilirken hata oluştu: {ex.Message}");
            }
        }

        private List<DepartmentDropdownDto> BuildHierarchicalDropdown(List<DepartmentDropdownDto> allDepartments, Guid? parentId)
        {
            return allDepartments
                .Where(d => GetParentId(d, allDepartments) == parentId)
                .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
                .Select(d =>
                {
                    d.SubDepartments = BuildHierarchicalDropdown(allDepartments, d.Id);
                    return d;
                })
                .ToList();
        }

        private Guid? GetParentId(DepartmentDropdownDto department, List<DepartmentDropdownDto> allDepartments)
        {
            return department.ParentDepartmentId;
        }

        private async Task<List<DepartmentDropdownDto>> MapSubDepartmentsRecursiveAsync(IEnumerable<Department> subDepartments)
        {
            var result = new List<DepartmentDropdownDto>();
            foreach (var subDept in subDepartments)
            {
                var dto = _mapper.Map<DepartmentDropdownDto>(subDept);
                if (subDept.SubDepartments != null && subDept.SubDepartments.Any())
                {
                    dto.SubDepartments = await MapSubDepartmentsRecursiveAsync(subDept.SubDepartments);
                }
                result.Add(dto);
            }
            return result;
        }

        public async Task<ApiResult<IEnumerable<DepartmentDto>>> GetSubDepartmentsAsync(Guid parentId)
        {
            try
            {
                var departments = await _departmentRepository.GetSubDepartmentsAsync(parentId);
                var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
                return ApiResult<IEnumerable<DepartmentDto>>.SuccessResult(departmentDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DepartmentDto>>.ErrorResult($"Müdürlükler getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<PagedResult<DepartmentDto>>> GetFilteredDepartmentsAsync(
            string? name = null, 
            int pageNumber = 1, 
            int pageSize = 10)
        {
            try
            {
                // Tüm departmanları al
                var allDepartments = await _departmentRepository.GetDepartmentsWithDetailsAsync();

                // Filtreleme uygula
                var filteredDepartments = allDepartments.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    filteredDepartments = filteredDepartments.Where(d => 
                        d.Name.ToLower().Contains(name.ToLower()));
                }

                // Önce DTOs'a çevir
                var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(filteredDepartments.ToList());
                
                // Hiyerarşik sırala
                var sortedDepartments = FlattenDepartments(departmentDtos);
                var totalCount = sortedDepartments.Count();

                // Sayfalama uygula
                var pagedDepartments = sortedDepartments
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                
                // PagedResult oluştur
                var pagedResult = new PagedResult<DepartmentDto>(pagedDepartments, totalCount, pageNumber, pageSize);
                
                return ApiResult<PagedResult<DepartmentDto>>.SuccessResult(pagedResult, 
                    $"Toplam {totalCount} departman bulundu, sayfa {pageNumber}/{pagedResult.TotalPages} gösteriliyor");
            }
            catch (Exception ex)
            {
                return ApiResult<PagedResult<DepartmentDto>>.ErrorResult($"Departmanlar filtrelenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DepartmentDto>> AssignManagerAsync(AssignManagerDto assignManagerDto)
        {
            try
            {
                var department = await _departmentRepository.GetByIdAsync(assignManagerDto.DepartmentId);
                if (department == null)
                    return ApiResult<DepartmentDto>.ErrorResult("Departman bulunamadı");

                department.ManagerPersonnelId = assignManagerDto.ManagerPersonnelId;
                department.UpdatedAt = DateTime.Now;

                var updatedDepartment = await _departmentRepository.UpdateAsync(department);
                var departmentDto = _mapper.Map<DepartmentDto>(updatedDepartment);
                
                string message = assignManagerDto.ManagerPersonnelId.HasValue 
                    ? "Departman yöneticisi başarıyla atandı" 
                    : "Departman yöneticisi başarıyla kaldırıldı";
                
                return ApiResult<DepartmentDto>.SuccessResult(departmentDto, message);
            }
            catch (Exception ex)
            {
                return ApiResult<DepartmentDto>.ErrorResult($"Yönetici atama işlemi sırasında hata oluştu: {ex.Message}");
            }
        }
    }
} 