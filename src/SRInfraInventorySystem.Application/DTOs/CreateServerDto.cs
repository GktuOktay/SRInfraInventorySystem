using SRInfraInventorySystem.Core.Enums;
using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Yeni sunucu oluşturmak için kullanılan DTO.
    /// </summary>
    public class CreateServerDto
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
    }
} 