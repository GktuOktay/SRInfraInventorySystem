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
    public class AccessLogRepository : GenericRepository<AccessLog>, IAccessLogRepository
    {
        public AccessLogRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsWithDetailsAsync()
        {
            return await _dbSet
                .Where(al => !al.IsDeleted)
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsByUserAsync(string userName)
        {
            return await _dbSet
                .Where(al => !al.IsDeleted && al.UserName.Contains(userName))
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(al => !al.IsDeleted && al.AccessDate >= startDate && al.AccessDate <= endDate)
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsByServerIdAsync(Guid serverId)
        {
            return await _dbSet
                .Where(al => !al.IsDeleted && al.ServerId == serverId)
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsByApplicationIdAsync(Guid applicationId)
        {
            return await _dbSet
                .Where(al => !al.IsDeleted && al.ApplicationId == applicationId)
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsByDatabaseIdAsync(Guid databaseId)
        {
            return await _dbSet
                .Where(al => !al.IsDeleted && al.DatabaseId == databaseId)
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsByUserNameAsync(string userName)
        {
            return await GetAccessLogsByUserAsync(userName);
        }

        public async Task<IEnumerable<AccessLog>> GetActiveAccessLogsAsync()
        {
            return await _dbSet
                .Where(al => !al.IsDeleted)
                .Include(al => al.Server)
                .Include(al => al.Application)
                .Include(al => al.Database)
                .OrderByDescending(al => al.AccessDate)
                .ToListAsync();
        }
    }
} 