using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;

namespace SRInfraInventorySystem.Application.Mapping
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            // Department -> DepartmentDto (Full mapping for listing)
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.ParentDepartmentName, opt => opt.MapFrom(src => src.ParentDepartment != null ? src.ParentDepartment.Name : null))
                .ForMember(dest => dest.ManagerPersonnelName, opt => opt.MapFrom(src => src.ManagerPersonnel != null ? $"{src.ManagerPersonnel.FirstName} {src.ManagerPersonnel.LastName}" : null))
                .ForMember(dest => dest.PersonnelCount, opt => opt.MapFrom(src => src.Personnel != null ? src.Personnel.Count : 0))
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore());

            // Department -> DepartmentResponseDto (Simple mapping for POST/PUT responses)
            CreateMap<Department, DepartmentResponseDto>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt));

            // Department -> DepartmentDropdownDto (Hierarchical mapping for select list)
            CreateMap<Department, DepartmentDropdownDto>()
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore()); // Manual mapping kullanacağız

            // Department -> HierarchicalDepartmentDto (Hiyerarşik yapı için)
            CreateMap<Department, HierarchicalDepartmentDto>()
                .ForMember(dest => dest.ManagerPersonnelName, opt => opt.MapFrom(src => src.ManagerPersonnel != null ? (src.ManagerPersonnel.FirstName + " " + src.ManagerPersonnel.LastName) : null))
                .ForMember(dest => dest.PersonnelCount, opt => opt.MapFrom(src => src.Personnel != null ? src.Personnel.Count : 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore());

            // CreateDepartmentDto -> Department
            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ManagerPersonnelId, opt => opt.Ignore()) // Yönetici ayrı endpoint'te atanacak
                .ForMember(dest => dest.ParentDepartment, opt => opt.Ignore())
                .ForMember(dest => dest.ManagerPersonnel, opt => opt.Ignore())
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore())
                .ForMember(dest => dest.Personnel, opt => opt.Ignore());

            // UpdateDepartmentDto -> Department
            CreateMap<UpdateDepartmentDto, Department>();
        }
    }
} 