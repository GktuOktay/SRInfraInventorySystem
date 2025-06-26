using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Yeni personel oluşturmak için kullanılan DTO.
    /// </summary>
    public class CreatePersonnelDto
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
        /// Personelin kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Personelin bağlı olduğu departmanın ID'si
        /// </summary>
        public Guid DepartmentId { get; set; }
    }
} 