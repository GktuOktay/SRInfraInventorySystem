using Microsoft.EntityFrameworkCore;
using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Infrastructure.Persistence;
using SRInfraInventorySystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Implementations
{
    public class DatabaseRepository : GenericRepository<Database>, IDatabaseRepository
    {
        public DatabaseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> HasRelatedDataAsync(Guid id)
        {
            // Access log'larını kontrol et (sadece silinmemiş olanlar)
            var hasAccessLogs = await _context.Set<AccessLog>()
                .Where(a => !a.IsDeleted)
                .AnyAsync(a => a.DatabaseId == id);

            return hasAccessLogs;
        }

        public async Task<IEnumerable<Database>> GetDatabasesWithDetailsAsync()
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .Include(d => d.Server)
                .Include(d => d.ConnectedApplication)
                .Include(d => d.AccessLogs.Where(al => !al.IsDeleted))
                .ToListAsync();
        }

        public async Task<Database> GetDatabaseWithDetailsByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .Include(d => d.Server)
                .Include(d => d.ConnectedApplication)
                .Include(d => d.AccessLogs.Where(al => !al.IsDeleted))
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Database>> GetDatabasesByServerAsync(Guid serverId)
        {
            return await _dbSet
                .Include(d => d.Server)
                .Where(d => d.ServerId == serverId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Database>> GetDatabasesByTypeAsync(string type)
        {
            return await _dbSet
                .Where(d => d.Type.Contains(type))
                .ToListAsync();
        }
    }
} 