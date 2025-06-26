using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Departman bilgilerini transfer etmek için kullanılan DTO.
    /// Hiyerarşik yapı ve temel departman bilgilerini içerir.
    /// </summary>
    public class DepartmentDto
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
        /// Üst departmanın ID'si (hiyerarşik yapı için)
        /// </summary>
        public Guid? ParentDepartmentId { get; set; }
        
        /// <summary>
        /// Üst departmanın adı
        /// </summary>
        public string ParentDepartmentName { get; set; }
        
        /// <summary>
        /// Departman yöneticisinin personel ID'si
        /// </summary>
        public Guid? ManagerPersonnelId { get; set; }
        
        /// <summary>
        /// Departman yöneticisinin adı
        /// </summary>
        public string ManagerPersonnelName { get; set; }
        
        /// <summary>
        /// Departmanın aktif olup olmadığını belirten flag
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Departmanda çalışan personel sayısı
        /// </summary>
        public int PersonnelCount { get; set; }
        
        /// <summary>
        /// Departmanın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Departmanın son güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        
        /// <summary>
        /// Alt departmanların listesi
        /// </summary>
        public List<DepartmentDto> SubDepartments { get; set; }
        
        /// <summary>
        /// DepartmentDto'nun yeni bir örneğini oluşturur
        /// </summary>
        public DepartmentDto()
        {
            SubDepartments = new List<DepartmentDto>();
        }
    }
} 