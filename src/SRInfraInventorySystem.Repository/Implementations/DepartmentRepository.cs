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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> HasRelatedDataAsync(Guid id)
        {
            // Alt departmanları kontrol et (sadece silinmemiş olanlar)
            var hasSubDepartments = await _dbSet
                .Where(d => !d.IsDeleted)
                .AnyAsync(d => d.ParentDepartmentId == id);

            // Personelleri kontrol et (sadece silinmemiş olanlar)
            var hasPersonnel = await _context.Set<Personnel>()
                .Where(p => !p.IsDeleted)
                .AnyAsync(p => p.DepartmentId == id);

            // Manager olarak atanmış olup olmadığını kontrol et (sadece silinmemiş olanlar)
            var isManagerOf = await _dbSet
                .Where(d => !d.IsDeleted)
                .AnyAsync(d => d.ManagerPersonnelId != null && 
                              _context.Set<Personnel>()
                                  .Where(p => !p.IsDeleted)
                                  .Any(p => p.Id == d.ManagerPersonnelId && p.DepartmentId == id));

            return hasSubDepartments || hasPersonnel || isManagerOf;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithDetailsAsync()
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .Include(d => d.ParentDepartment)
                .Include(d => d.SubDepartments.Where(sd => !sd.IsDeleted))
                .Include(d => d.ManagerPersonnel)
                .Include(d => d.Personnel.Where(p => !p.IsDeleted))
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsWithDetailsAsync()
        {
            return await _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.SubDepartments)
                .Include(d => d.ManagerPersonnel)
                .Include(d => d.Personnel)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentWithDetailsByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .Include(d => d.ParentDepartment)
                .Include(d => d.SubDepartments.Where(sd => !sd.IsDeleted))
                .Include(d => d.ManagerPersonnel)
                .Include(d => d.Personnel.Where(p => !p.IsDeleted))
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> GetRootDepartmentsAsync()
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .Include(d => d.SubDepartments.Where(sd => !sd.IsDeleted))
                .Where(d => d.ParentDepartmentId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetSubDepartmentsAsync(Guid parentId)
        {
            return await _dbSet
                .Where(d => !d.IsDeleted)
                .Include(d => d.SubDepartments.Where(sd => !sd.IsDeleted))
                .Where(d => d.ParentDepartmentId == parentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllSubDepartmentsRecursiveAsync(Guid parentId)
        {
            var allSubDepartments = new List<Department>();
            var directSubDepartments = await _dbSet
                .Where(d => d.ParentDepartmentId == parentId)
                .ToListAsync();

            foreach (var subDept in directSubDepartments)
            {
                allSubDepartments.Add(subDept);
                // Recursive olarak alt departmanları da getir
                var nestedSubDepts = await GetAllSubDepartmentsRecursiveAsync(subDept.Id);
                allSubDepartments.AddRange(nestedSubDepts);
            }

            return allSubDepartments;
        }

        public async Task DeleteDepartmentWithSubDepartmentsAsync(Guid departmentId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Önce tüm alt departmanları recursive olarak getir
                var allSubDepartments = await GetAllSubDepartmentsRecursiveAsync(departmentId);
                
                // Alt departmanları sil (en derinden başlayarak)
                var sortedSubDepartments = allSubDepartments
                    .OrderByDescending(d => GetDepartmentDepth(d, allSubDepartments.ToList()))
                    .ToList();

                foreach (var subDept in sortedSubDepartments)
                {
                    _dbSet.Remove(subDept);
                }

                // Ana departmanı sil
                var mainDepartment = await _dbSet.FindAsync(departmentId);
                if (mainDepartment != null)
                {
                    _dbSet.Remove(mainDepartment);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private int GetDepartmentDepth(Department department, List<Department> allDepartments)
        {
            int depth = 0;
            var current = department;

            while (current?.ParentDepartmentId != null)
            {
                depth++;
                current = allDepartments.FirstOrDefault(d => d.Id == current.ParentDepartmentId);
            }

            return depth;
        }
    }
} 