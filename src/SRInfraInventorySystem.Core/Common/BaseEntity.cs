using System;

namespace SRInfraInventorySystem.Core.Common
{
    /// <summary>
    /// Tüm entity'ler için temel sınıf. Ortak özellikleri içerir.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity'nin benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Entity'nin oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Entity'nin son güncellenme tarihi (null ise hiç güncellenmemiş)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// Entity'nin silinip silinmediğini belirten flag (soft delete için)
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Yerel saati döndürür
        /// </summary>
        /// <returns>Yerel saat</returns>
        protected static DateTime GetLocalTime()
        {
            return DateTime.Now;
        }
    }
} 