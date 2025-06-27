using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Yeni departman oluşturmak için kullanılan DTO.
    /// Sadece gerekli alanları içerir.
    /// </summary>
    public class CreateDepartmentDto
    {
        /// <summary>
        /// Departmanın adı (zorunlu)
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Departmanın açıklaması (opsiyonel)
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Üst departmanın ID'si (hiyerarşik yapı için, opsiyonel)
        /// </summary>
        public Guid? ParentDepartmentId { get; set; }
    }
} 