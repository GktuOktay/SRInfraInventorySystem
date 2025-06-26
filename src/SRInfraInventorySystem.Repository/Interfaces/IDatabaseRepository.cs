using SRInfraInventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Veritabanı entity'si için özel repository interface'i.
    /// Veritabanlarına özgü işlemleri tanımlar.
    /// </summary>
    public interface IDatabaseRepository : IGenericRepository<Database>
    {
        /// <summary>
        /// Tüm veritabanlarını detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı veritabanı listesi</returns>
        Task<IEnumerable<Database>> GetDatabasesWithDetailsAsync();
        Task<Database> GetDatabaseWithDetailsByIdAsync(Guid id);
        Task<IEnumerable<Database>> GetDatabasesByServerAsync(Guid serverId);
        Task<IEnumerable<Database>> GetDatabasesByTypeAsync(string type);
    }
} 