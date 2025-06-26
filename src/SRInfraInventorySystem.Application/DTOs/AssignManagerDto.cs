using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Departmana yönetici atamak için kullanılan DTO.
    /// </summary>
    public class AssignManagerDto
    {
        /// <summary>
        /// Yönetici atanacak departmanın ID'si
        /// </summary>
        public Guid DepartmentId { get; set; }
        
        /// <summary>
        /// Atanacak yöneticinin personel ID'si
        /// </summary>
        public Guid? ManagerPersonnelId { get; set; }
    }
} 