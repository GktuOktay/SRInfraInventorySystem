using SRInfraInventorySystem.Core.Common;
using System;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Organizasyon içindeki personelleri temsil eden entity.
    /// Kişisel bilgiler, departman ilişkisi ve kimlik doğrulama bilgilerini içerir.
    /// </summary>
    public class Personnel : BaseEntity
    {
        /// <summary>
        /// Personelin adı
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Personelin soyadı
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Personelin e-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Personelin telefon numarası
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Personelin unvanı/pozisyonu
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Personelin kimlik numarası
        /// </summary>
        public string IdentityNumber { get; set; }
        
        /// <summary>
        /// Personelin kullanıcı adı (sistem erişimi için)
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Personelin bağlı olduğu departmanın ID'si
        /// </summary>
        public Guid DepartmentId { get; set; }
        
        // Navigation Properties
        
        /// <summary>
        /// Personelin bağlı olduğu departman
        /// </summary>
        public Department Department { get; set; }
    }
} 