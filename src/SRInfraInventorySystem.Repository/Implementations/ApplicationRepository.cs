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
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> HasRelatedDataAsync(Guid id)
        {
            // Bağlı veritabanlarını kontrol et (sadece silinmemiş olanlar)
            var hasConnectedDatabases = await _context.Set<Database>()
                .Where(d => !d.IsDeleted)
                .AnyAsync(d => d.ApplicationId == id);

            // Access log'larını kontrol et (sadece silinmemiş olanlar)
            var hasAccessLogs = await _context.Set<AccessLog>()
                .Where(a => !a.IsDeleted)
                .AnyAsync(a => a.ApplicationId == id);

            return hasConnectedDatabases || hasAccessLogs;
        }

        public async Task<IEnumerable<Application>> GetApplicationsWithDetailsAsync()
        {
            return await _dbSet
                .Where(a => !a.IsDeleted)
                .Include(a => a.Server)
                .Include(a => a.ConnectedDatabases.Where(d => !d.IsDeleted))
                .Include(a => a.AccessLogs.Where(al => !al.IsDeleted))
                .ToListAsync();
        }

        public async Task<Application> GetApplicationWithDetailsByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(a => !a.IsDeleted)
                .Include(a => a.Server)
                .Include(a => a.ConnectedDatabases.Where(d => !d.IsDeleted))
                .Include(a => a.AccessLogs.Where(al => !al.IsDeleted))
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Application>> GetApplicationsByServerAsync(Guid serverId)
        {
            return await _dbSet
                .Include(a => a.Server)
                .Where(a => a.ServerId == serverId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationsByStatusAsync(ApplicationStatus status)
        {
            return await _dbSet
                .Where(a => a.Status == status)
                .ToListAsync();
        }
    }
} 