using SRInfraInventorySystem.Core.Common;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Organizasyon içindeki departmanları temsil eden entity.
    /// Hiyerarşik yapıyı destekler ve personel yönetimi sağlar.
    /// </summary>
    public class Department : BaseEntity
    {
        /// <summary>
        /// Departmanın adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Departmanın açıklaması
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Üst departmanın ID'si (hiyerarşik yapı için)
        /// </summary>
        public Guid? ParentDepartmentId { get; set; }
        
        /// <summary>
        /// Departman yöneticisinin personel ID'si
        /// </summary>
        public Guid? ManagerPersonnelId { get; set; }
        
        /// <summary>
        /// Departmanın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        
        /// <summary>
        /// Üst departman (hiyerarşik yapı için)
        /// </summary>
        public Department? ParentDepartment { get; set; }
        
        /// <summary>
        /// Departman yöneticisi
        /// </summary>
        public Personnel? ManagerPersonnel { get; set; }
        
        /// <summary>
        /// Alt departmanların koleksiyonu
        /// </summary>
        public ICollection<Department> SubDepartments { get; set; }
        
        /// <summary>
        /// Departmanda çalışan personellerin koleksiyonu
        /// </summary>
        public ICollection<Personnel> Personnel { get; set; }

        /// <summary>
        /// Department entity'sinin yeni bir örneğini oluşturur
        /// </summary>
        public Department()
        {
            SubDepartments = new HashSet<Department>();
            Personnel = new HashSet<Personnel>();
        }
    }
} 