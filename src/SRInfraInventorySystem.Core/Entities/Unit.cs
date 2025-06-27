using SRInfraInventorySystem.Core.Common;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Organizasyon içindeki birimleri temsil eden entity.
    /// Departmanlardan daha küçük organizasyonel birimler için kullanılır.
    /// </summary>
    public class Unit : BaseEntity
    {
        /// <summary>
        /// Birimin adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Birimin açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Birimin bağlı olduğu departmanın ID'si
        /// </summary>
        public Guid DepartmentId { get; set; }
        
        /// <summary>
        /// Birimin aktif olup olmadığını belirten flag
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Birimin oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Foreign Keys
        public Guid? SupervisorPersonnelId { get; set; }

        // Navigation Properties
        
        /// <summary>
        /// Birimin bağlı olduğu departman
        /// </summary>
        public Department Department { get; set; }
        public virtual Personnel SupervisorPersonnel { get; set; }
        public virtual ICollection<Personnel> Personnel { get; set; }

        public Unit()
        {
            Personnel = new HashSet<Personnel>();
        }
    }
} 