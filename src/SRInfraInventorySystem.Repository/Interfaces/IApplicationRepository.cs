using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Uygulama entity'si için özel repository interface'i.
    /// Uygulamalara özgü işlemleri tanımlar.
    /// </summary>
    public interface IApplicationRepository : IGenericRepository<Application>
    {
        /// <summary>
        /// Tüm uygulamaları detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı uygulama listesi</returns>
        Task<IEnumerable<Application>> GetApplicationsWithDetailsAsync();
        Task<Application> GetApplicationWithDetailsByIdAsync(Guid id);
        Task<IEnumerable<Application>> GetApplicationsByServerAsync(Guid serverId);
        Task<IEnumerable<Application>> GetApplicationsByStatusAsync(ApplicationStatus status);
    }
} 