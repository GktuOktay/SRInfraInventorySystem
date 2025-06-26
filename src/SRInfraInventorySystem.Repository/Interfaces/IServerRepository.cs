using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Sunucu entity'si için özel repository interface'i.
    /// Sunuculara özgü işlemleri tanımlar.
    /// </summary>
    public interface IServerRepository : IGenericRepository<Server>
    {
        /// <summary>
        /// Tüm sunucuları detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı sunucu listesi</returns>
        Task<IEnumerable<Server>> GetServersWithDetailsAsync();
        
        /// <summary>
        /// Belirtilen ID'ye sahip sunucuyu detaylarıyla birlikte getirir
        /// </summary>
        /// <param name="id">Sunucu ID'si</param>
        /// <returns>Detaylı sunucu bilgisi</returns>
        Task<Server> GetServerWithDetailsByIdAsync(Guid id);
        
        /// <summary>
        /// Belirtilen çevredeki sunucuları getirir
        /// </summary>
        /// <param name="environment">Sunucu çevresi</param>
        /// <returns>Çevreye ait sunucuların listesi</returns>
        Task<IEnumerable<Server>> GetServersByEnvironmentAsync(string environment);

        Task<IEnumerable<Server>> GetServersByStatusAsync(ServerStatus status);
        Task<IEnumerable<Server>> GetServersByOperatingSystemAsync(string operatingSystem);
    }
} 