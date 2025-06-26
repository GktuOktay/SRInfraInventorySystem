using SRInfraInventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Sunucu kullanım geçmişi entity'si için özel repository interface'i.
    /// Sunucu kullanım geçmişine özgü işlemleri tanımlar.
    /// </summary>
    public interface IServerUsageHistoryRepository : IGenericRepository<ServerUsageHistory>
    {
        /// <summary>
        /// Tüm sunucu kullanım geçmişini detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı sunucu kullanım geçmişi listesi</returns>
        Task<IEnumerable<ServerUsageHistory>> GetServerUsageHistoryWithDetailsAsync();
        
        /// <summary>
        /// Belirtilen sunucunun kullanım geçmişini getirir
        /// </summary>
        /// <param name="serverId">Sunucu ID'si</param>
        /// <returns>Sunucunun kullanım geçmişinin listesi</returns>
        Task<IEnumerable<ServerUsageHistory>> GetServerUsageHistoryByServerAsync(Guid serverId);
        
        /// <summary>
        /// Belirtilen tarih aralığındaki sunucu kullanım geçmişini getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Tarih aralığındaki kullanım geçmişinin listesi</returns>
        Task<IEnumerable<ServerUsageHistory>> GetServerUsageHistoryByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Eski method'lar (geriye uyumluluk için)
        Task<IEnumerable<ServerUsageHistory>> GetHistoryByServerIdAsync(Guid serverId);
        Task<IEnumerable<ServerUsageHistory>> GetHistoryByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ServerUsageHistory> GetLatestUsageByServerIdAsync(Guid serverId);
        Task<IEnumerable<ServerUsageHistory>> GetUsageHistoryWithServerDetailsAsync();
        Task<double> GetAverageCpuUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate);
        Task<double> GetAverageMemoryUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate);
    }
} 