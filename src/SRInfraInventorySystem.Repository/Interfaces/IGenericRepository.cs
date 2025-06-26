using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Interfaces
{
    /// <summary>
    /// Generic repository pattern için temel interface.
    /// Tüm entity'ler için ortak CRUD operasyonlarını tanımlar.
    /// </summary>
    /// <typeparam name="T">Repository'nin çalışacağı entity tipi</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Belirtilen ID'ye sahip entity'yi getirir
        /// </summary>
        /// <param name="id">Entity'nin benzersiz tanımlayıcısı</param>
        /// <returns>Bulunan entity veya null</returns>
        Task<T> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Tüm entity'leri getirir
        /// </summary>
        /// <returns>Entity'lerin koleksiyonu</returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Belirtilen koşula uyan entity'leri getirir
        /// </summary>
        /// <param name="predicate">Filtreleme koşulu</param>
        /// <returns>Koşula uyan entity'lerin koleksiyonu</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Yeni entity ekler
        /// </summary>
        /// <param name="entity">Eklenecek entity</param>
        /// <returns>Eklenen entity</returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Mevcut entity'yi günceller
        /// </summary>
        /// <param name="entity">Güncellenecek entity</param>
        /// <returns>Güncellenen entity</returns>
        Task<T> UpdateAsync(T entity);
        
        /// <summary>
        /// Entity'yi siler (soft delete)
        /// </summary>
        /// <param name="entity">Silinecek entity</param>
        Task DeleteAsync(T entity);
        
        /// <summary>
        /// Belirtilen koşula uyan entity'nin var olup olmadığını kontrol eder
        /// </summary>
        /// <param name="predicate">Kontrol koşulu</param>
        /// <returns>Entity varsa true, yoksa false</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Entity'ler için IQueryable döndürür
        /// </summary>
        /// <returns>IQueryable nesnesi</returns>
        IQueryable<T> Query();
        
        /// <summary>
        /// Belirtilen ID'ye sahip entity'nin ilişkili verisi olup olmadığını kontrol eder
        /// </summary>
        /// <param name="id">Entity'nin benzersiz tanımlayıcısı</param>
        /// <returns>İlişkili veri varsa true, yoksa false</returns>
        Task<bool> HasRelatedDataAsync(Guid id);
    }
} 