using SRInfraInventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Personel entity'si için özel repository interface'i.
    /// Personellere özgü işlemleri tanımlar.
    /// </summary>
    public interface IPersonnelRepository : IGenericRepository<Personnel>
    {
        /// <summary>
        /// Tüm personelleri detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı personel listesi</returns>
        Task<IEnumerable<Personnel>> GetPersonnelWithDetailsAsync();
        
        /// <summary>
        /// Belirtilen departmandaki personelleri getirir
        /// </summary>
        /// <param name="departmentId">Departman ID'si</param>
        /// <returns>Departmandaki personellerin listesi</returns>
        Task<IEnumerable<Personnel>> GetPersonnelByDepartmentAsync(Guid departmentId);

        Task<Personnel> GetPersonnelWithDetailsByIdAsync(Guid id);
        Task<IEnumerable<Personnel>> GetActivePersonnelAsync();
        Task<Personnel> GetPersonnelByUserNameAsync(string userName);
    }
} 