using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Mevcut departmanı güncellemek için kullanılan DTO.
    /// Tüm alanlar opsiyoneldir, sadece değiştirilecek alanlar gönderilir.
    /// </summary>
    public class UpdateDepartmentDto
    {
        /// <summary>
        /// Departmanın yeni adı
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Departmanın yeni açıklaması
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Yeni üst departmanın ID'si
        /// </summary>
        public Guid? ParentDepartmentId { get; set; }
        
        /// <summary>
        /// Yeni departman yöneticisinin personel ID'si
        /// </summary>
        public Guid? ManagerPersonnelId { get; set; }
        
        /// <summary>
        /// Departmanın aktif durumu
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
} 