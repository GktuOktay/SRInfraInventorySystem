using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Personel bilgilerini transfer etmek için kullanılan DTO.
    /// Kişisel bilgiler ve departman ilişkisini içerir.
    /// </summary>
    public class PersonnelDto
    {
        /// <summary>
        /// Personelin benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Personelin adı
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Personelin soyadı
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Personelin tam adı (Ad Soyad)
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
        
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
        public Guid? DepartmentId { get; set; }
        
        /// <summary>
        /// Personelin bağlı olduğu departmanın adı
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// Personelin oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Personelin son güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
} 