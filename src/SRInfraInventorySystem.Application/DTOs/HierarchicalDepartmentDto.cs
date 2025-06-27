using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Hiyerarşik departman yapısı için kullanılan DTO.
    /// </summary>
    public class HierarchicalDepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public Guid? ManagerPersonnelId { get; set; }
        public string ManagerPersonnelName { get; set; }
        public int PersonnelCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<HierarchicalDepartmentDto> SubDepartments { get; set; } = new();
    }
} 