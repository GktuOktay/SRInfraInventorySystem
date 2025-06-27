using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Departman oluşturma işlemi sonrasında döndürülen basit DTO.
    /// Sadece temel bilgileri içerir.
    /// </summary>
    public class DepartmentResponseDto
    {
        /// <summary>
        /// Departmanın benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Departmanın adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Departmanın açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Departmanın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Departmanın güncellenme tarihi
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
} 