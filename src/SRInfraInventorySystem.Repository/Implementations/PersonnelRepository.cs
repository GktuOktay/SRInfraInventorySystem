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
    public class PersonnelRepository : GenericRepository<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> HasRelatedDataAsync(Guid id)
        {
            // Manager olduğu departmanları kontrol et (sadece silinmemiş olanlar)
            var isManagerOf = await _context.Set<Department>()
                .Where(d => !d.IsDeleted)
                .AnyAsync(d => d.ManagerPersonnelId == id);

            return isManagerOf;
        }

        public async Task<IEnumerable<Personnel>> GetPersonnelWithDetailsAsync()
        {
            return await _dbSet
                .Where(p => !p.IsDeleted)
                .Include(p => p.Department)
                .ToListAsync();
        }

        public async Task<Personnel> GetPersonnelWithDetailsByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(p => !p.IsDeleted)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Personnel>> GetPersonnelByDepartmentAsync(Guid departmentId)
        {
            return await _dbSet
                .Where(p => !p.IsDeleted && p.DepartmentId == departmentId)
                .Include(p => p.Department)
                .ToListAsync();
        }

        public async Task<IEnumerable<Personnel>> GetActivePersonnelAsync()
        {
            return await _dbSet
                .Where(p => !p.IsDeleted)
                .Include(p => p.Department)
                .ToListAsync();
        }

        public async Task<Personnel> GetPersonnelByUserNameAsync(string userName)
        {
            return await _dbSet
                .Where(p => !p.IsDeleted)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.UserName == userName);
        }
    }
} 