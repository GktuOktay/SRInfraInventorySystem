using Microsoft.EntityFrameworkCore;
using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Core.Enums;
using SRInfraInventorySystem.Infrastructure.Persistence;
using SRInfraInventorySystem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Repository.Implementations
{
    public class ServerRepository : GenericRepository<Server>, IServerRepository
    {
        public ServerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> HasRelatedDataAsync(Guid id)
        {
            // Bağlı uygulamaları kontrol et (sadece silinmemiş olanlar)
            var hasApplications = await _context.Set<Application>()
                .Where(a => !a.IsDeleted)
                .AnyAsync(a => a.ServerId == id);

            // Bağlı veritabanlarını kontrol et (sadece silinmemiş olanlar)
            var hasDatabases = await _context.Set<Database>()
                .Where(d => !d.IsDeleted)
                .AnyAsync(d => d.ServerId == id);

            // Kullanım geçmişini kontrol et (sadece silinmemiş olanlar)
            var hasUsageHistory = await _context.Set<ServerUsageHistory>()
                .Where(h => !h.IsDeleted)
                .AnyAsync(h => h.ServerId == id);

            return hasApplications || hasDatabases || hasUsageHistory;
        }

        public async Task<IEnumerable<Server>> GetServersWithDetailsAsync()
        {
            return await _dbSet
                .Where(s => !s.IsDeleted)
                .Include(s => s.Applications.Where(a => !a.IsDeleted))
                .Include(s => s.Databases.Where(d => !d.IsDeleted))
                .Include(s => s.UsageHistory.Where(h => !h.IsDeleted))
                .ToListAsync();
        }

        public async Task<Server> GetServerWithDetailsByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(s => !s.IsDeleted)
                .Include(s => s.Applications.Where(a => !a.IsDeleted))
                .Include(s => s.Databases.Where(d => !d.IsDeleted))
                .Include(s => s.UsageHistory.Where(h => !h.IsDeleted))
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Server>> GetServersByEnvironmentAsync(string environment)
        {
            if (Enum.TryParse<ServerEnvironment>(environment, true, out var env))
            {
                return await _dbSet
                    .Where(s => !s.IsDeleted && s.Environment == env)
                    .ToListAsync();
            }
            
            return new List<Server>();
        }

        public async Task<IEnumerable<Server>> GetServersByStatusAsync(ServerStatus status)
        {
            return await _dbSet
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Server>> GetServersByOperatingSystemAsync(string operatingSystem)
        {
            return await _dbSet
                .Where(s => s.OperatingSystem.Contains(operatingSystem))
                .ToListAsync();
        }
    }
} 