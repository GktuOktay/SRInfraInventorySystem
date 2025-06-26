using Microsoft.EntityFrameworkCore;
using SRInfraInventorySystem.Core.Common;
using SRInfraInventorySystem.Infrastructure.Persistence;
using SRInfraInventorySystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Sadece silinmemiş kayıtları getir
            return await _dbSet
                .Where(GetNotDeletedExpression())
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // Predicate'e IsDeleted = false koşulunu ekle
            var combinedPredicate = CombineWithNotDeleted(predicate);
            return await _dbSet.Where(combinedPredicate).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            // UpdatedAt alanını otomatik güncelle
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.UpdatedAt = DateTime.UtcNow;
            }
            
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            // Soft delete - IsDeleted = true yap
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.UpdatedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            // Predicate'e IsDeleted = false koşulunu ekle
            var combinedPredicate = CombineWithNotDeleted(predicate);
            return await _dbSet.AnyAsync(combinedPredicate);
        }

        public IQueryable<T> Query()
        {
            // Sadece silinmemiş kayıtları döndür
            return _dbSet.Where(GetNotDeletedExpression());
        }

        public virtual async Task<bool> HasRelatedDataAsync(Guid id)
        {
            // Varsayılan implementasyon - türetilmiş sınıflarda override edilecek
            return false;
        }

        // Soft delete için yardımcı metodlar
        protected Expression<Func<T, bool>> GetNotDeletedExpression()
        {
            // T tipinin BaseEntity'den türetilip türetilmediğini kontrol et
            if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            {
                return entity => !((BaseEntity)(object)entity).IsDeleted;
            }
            return entity => true; // BaseEntity değilse tümünü getir
        }

        protected Expression<Func<T, bool>> CombineWithNotDeleted(Expression<Func<T, bool>> predicate)
        {
            var notDeletedExpression = GetNotDeletedExpression();
            
            // İki expression'ı birleştir
            var parameter = Expression.Parameter(typeof(T), "entity");
            var combined = Expression.AndAlso(
                Expression.Invoke(notDeletedExpression, parameter),
                Expression.Invoke(predicate, parameter)
            );
            
            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
    }
} 