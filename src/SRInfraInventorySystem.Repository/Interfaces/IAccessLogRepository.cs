using SRInfraInventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Erişim logu entity'si için özel repository interface'i.
    /// Erişim loglarına özgü işlemleri tanımlar.
    /// </summary>
    public interface IAccessLogRepository : IGenericRepository<AccessLog>
    {
        /// <summary>
        /// Tüm erişim loglarını detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı erişim logu listesi</returns>
        Task<IEnumerable<AccessLog>> GetAccessLogsWithDetailsAsync();
        
        /// <summary>
        /// Belirtilen kullanıcının erişim loglarını getirir
        /// </summary>
        /// <param name="userName">Kullanıcı adı</param>
        /// <returns>Kullanıcının erişim loglarının listesi</returns>
        Task<IEnumerable<AccessLog>> GetAccessLogsByUserAsync(string userName);
        
        /// <summary>
        /// Belirtilen tarih aralığındaki erişim loglarını getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Tarih aralığındaki erişim loglarının listesi</returns>
        Task<IEnumerable<AccessLog>> GetAccessLogsByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<AccessLog>> GetAccessLogsByServerIdAsync(Guid serverId);
        Task<IEnumerable<AccessLog>> GetAccessLogsByApplicationIdAsync(Guid applicationId);
        Task<IEnumerable<AccessLog>> GetAccessLogsByDatabaseIdAsync(Guid databaseId);
        Task<IEnumerable<AccessLog>> GetAccessLogsByUserNameAsync(string userName);
        Task<IEnumerable<AccessLog>> GetActiveAccessLogsAsync();
    }
} 