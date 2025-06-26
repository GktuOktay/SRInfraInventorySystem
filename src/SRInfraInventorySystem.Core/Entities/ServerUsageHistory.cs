using SRInfraInventorySystem.Core.Common;
using System;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Sunucu kullanım geçmişini takip eden entity.
    /// Sunucu performansı ve kullanım istatistikleri için kullanılır.
    /// </summary>
    public class ServerUsageHistory : BaseEntity
    {
        /// <summary>
        /// Kullanım tarihi
        /// </summary>
        public DateTime UsageDate { get; set; }
        
        /// <summary>
        /// CPU kullanım yüzdesi
        /// </summary>
        public double CpuUsage { get; set; }
        
        /// <summary>
        /// RAM kullanım yüzdesi
        /// </summary>
        public double MemoryUsage { get; set; }
        
        /// <summary>
        /// Disk kullanım yüzdesi
        /// </summary>
        public double DiskUsage { get; set; }
        
        /// <summary>
        /// Ağ kullanımı (MB/s)
        /// </summary>
        public double NetworkUsage { get; set; }
        
        /// <summary>
        /// Kullanım hakkında ek notlar
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Kullanım geçmişinin ait olduğu sunucunun ID'si
        /// </summary>
        public Guid ServerId { get; set; }
        
        // Navigation Properties
        
        /// <summary>
        /// Kullanım geçmişinin ait olduğu sunucu
        /// </summary>
        public Server Server { get; set; }
    }
} 