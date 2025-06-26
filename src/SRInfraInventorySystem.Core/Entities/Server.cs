using SRInfraInventorySystem.Core.Common;
using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Altyapı sistemlerindeki sunucuları temsil eden entity.
    /// Donanım bilgileri, çevre bilgileri ve ilişkili kaynakları içerir.
    /// </summary>
    public class Server : BaseEntity
    {
        /// <summary>
        /// Sunucunun adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Sunucunun IP adresi
        /// </summary>
        public string IpAddress { get; set; }
        
        /// <summary>
        /// Sunucunun işletim sistemi
        /// </summary>
        public string OperatingSystem { get; set; }
        
        /// <summary>
        /// CPU bilgileri
        /// </summary>
        public string CpuInfo { get; set; }
        
        /// <summary>
        /// RAM boyutu (GB cinsinden)
        /// </summary>
        public int RamSizeGB { get; set; }
        
        /// <summary>
        /// Disk boyutu (GB cinsinden)
        /// </summary>
        public int DiskSizeGB { get; set; }
        
        /// <summary>
        /// Sunucunun çalışma ortamı (Development, Staging, Production)
        /// </summary>
        public ServerEnvironment Environment { get; set; }
        
        /// <summary>
        /// Sunucunun durumu (Active, Inactive, Maintenance, Decommissioned)
        /// </summary>
        public ServerStatus Status { get; set; }
        
        /// <summary>
        /// Sunucudan sorumlu kişi
        /// </summary>
        public string ResponsiblePerson { get; set; }
        
        /// <summary>
        /// Sunucunun fiziksel konumu
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// Sunucu hakkında açıklama
        /// </summary>
        public string Description { get; set; }
        
        // Navigation Properties
        
        /// <summary>
        /// Sunucuda çalışan uygulamaların koleksiyonu
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; }
        
        /// <summary>
        /// Sunucuda bulunan veritabanlarının koleksiyonu
        /// </summary>
        public virtual ICollection<Database> Databases { get; set; }
        
        /// <summary>
        /// Sunucu kullanım geçmişinin koleksiyonu
        /// </summary>
        public virtual ICollection<ServerUsageHistory> UsageHistory { get; set; }
        
        /// <summary>
        /// Server entity'sinin yeni bir örneğini oluşturur
        /// </summary>
        public Server()
        {
            Applications = new HashSet<Application>();
            Databases = new HashSet<Database>();
            UsageHistory = new HashSet<ServerUsageHistory>();
        }
    }
} 