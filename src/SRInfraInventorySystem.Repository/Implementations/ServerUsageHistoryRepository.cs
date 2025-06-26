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
    public class ServerUsageHistoryRepository : GenericRepository<ServerUsageHistory>, IServerUsageHistoryRepository
    {
        public ServerUsageHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ServerUsageHistory>> GetServerUsageHistoryWithDetailsAsync()
        {
            return await _dbSet
                .Where(h => !h.IsDeleted)
                .Include(h => h.Server)
                .OrderByDescending(h => h.UsageDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServerUsageHistory>> GetServerUsageHistoryByServerAsync(Guid serverId)
        {
            return await _dbSet
                .Where(h => !h.IsDeleted && h.ServerId == serverId)
                .Include(h => h.Server)
                .OrderByDescending(h => h.UsageDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServerUsageHistory>> GetServerUsageHistoryByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(h => !h.IsDeleted && h.UsageDate >= startDate && h.UsageDate <= endDate)
                .Include(h => h.Server)
                .OrderByDescending(h => h.UsageDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServerUsageHistory>> GetHistoryByServerIdAsync(Guid serverId)
        {
            return await GetServerUsageHistoryByServerAsync(serverId);
        }

        public async Task<IEnumerable<ServerUsageHistory>> GetHistoryByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetServerUsageHistoryByDateRangeAsync(startDate, endDate);
        }

        public async Task<ServerUsageHistory> GetLatestUsageByServerIdAsync(Guid serverId)
        {
            return await _dbSet
                .Where(h => !h.IsDeleted && h.ServerId == serverId)
                .Include(h => h.Server)
                .OrderByDescending(h => h.UsageDate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ServerUsageHistory>> GetUsageHistoryWithServerDetailsAsync()
        {
            return await GetServerUsageHistoryWithDetailsAsync();
        }

        public async Task<double> GetAverageCpuUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate)
        {
            var usages = await _dbSet
                .Where(h => !h.IsDeleted && h.ServerId == serverId && h.UsageDate >= startDate && h.UsageDate <= endDate)
                .Select(h => h.CpuUsage)
                .ToListAsync();

            return usages.Any() ? usages.Average() : 0;
        }

        public async Task<double> GetAverageMemoryUsageByServerIdAsync(Guid serverId, DateTime startDate, DateTime endDate)
        {
            var usages = await _dbSet
                .Where(h => !h.IsDeleted && h.ServerId == serverId && h.UsageDate >= startDate && h.UsageDate <= endDate)
                .Select(h => h.MemoryUsage)
                .ToListAsync();

            return usages.Any() ? usages.Average() : 0;
        }
    }
} 