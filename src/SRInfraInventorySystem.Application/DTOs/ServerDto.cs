using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Sunucu bilgilerini transfer etmek için kullanılan DTO.
    /// Donanım bilgileri, çevre bilgileri ve ilişkili kaynakları içerir.
    /// </summary>
    public class ServerDto
    {
        /// <summary>
        /// Sunucunun benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
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
        /// Sunucunun çalışma ortamı
        /// </summary>
        public ServerEnvironment Environment { get; set; }
        
        /// <summary>
        /// Sunucunun durumu
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
        
        /// <summary>
        /// Sunucunun oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Sunucunun son güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        
        /// <summary>
        /// Sunucuda çalışan uygulamaların listesi
        /// </summary>
        public List<ApplicationDto> Applications { get; set; }
        
        /// <summary>
        /// Sunucuda bulunan veritabanlarının listesi
        /// </summary>
        public List<DatabaseDto> Databases { get; set; }
        
        /// <summary>
        /// Sunucu kullanım geçmişinin listesi
        /// </summary>
        public List<ServerUsageHistoryDto> UsageHistory { get; set; }
        
        /// <summary>
        /// ServerDto'nun yeni bir örneğini oluşturur
        /// </summary>
        public ServerDto()
        {
            Applications = new List<ApplicationDto>();
            Databases = new List<DatabaseDto>();
            UsageHistory = new List<ServerUsageHistoryDto>();
        }
    }
} 