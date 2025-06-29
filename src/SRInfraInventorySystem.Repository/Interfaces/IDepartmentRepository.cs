using SRInfraInventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Departman entity'si için özel repository interface'i.
    /// Departmanlara özgü işlemleri tanımlar.
    /// </summary>
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        /// <summary>
        /// Tüm departmanları detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı departman listesi</returns>
        Task<IEnumerable<Department>> GetDepartmentsWithDetailsAsync();
        
        /// <summary>
        /// Tüm departmanları detaylarıyla birlikte getirir
        /// </summary>
        /// <returns>Detaylı departman listesi</returns>
        Task<IEnumerable<Department>> GetAllDepartmentsWithDetailsAsync();
        
        /// <summary>
        /// Belirtilen ID'ye sahip departmanı detaylarıyla birlikte getirir
        /// </summary>
        /// <param name="id">Departman ID'si</param>
        /// <returns>Detaylı departman bilgisi</returns>
        Task<Department> GetDepartmentWithDetailsByIdAsync(Guid id);
        
        /// <summary>
        /// Ana departmanları getirir (parent departmanı olmayanlar)
        /// </summary>
        /// <returns>Ana departmanların listesi</returns>
        Task<IEnumerable<Department>> GetRootDepartmentsAsync();
        
        /// <summary>
        /// Belirtilen departmanın alt departmanlarını getirir
        /// </summary>
        /// <param name="parentId">Üst departman ID'si</param>
        /// <returns>Alt departmanların listesi</returns>
        Task<IEnumerable<Department>> GetSubDepartmentsAsync(Guid parentId);

        Task<IEnumerable<Department>> GetAllSubDepartmentsRecursiveAsync(Guid parentId);
        Task DeleteDepartmentWithSubDepartmentsAsync(Guid departmentId);
    }
} 