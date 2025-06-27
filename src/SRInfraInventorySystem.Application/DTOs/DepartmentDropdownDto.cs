using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Dropdown listeler için kullanılan basit departman DTO'su.
    /// Sadece ID ve ad bilgilerini içerir.
    /// </summary>
    public class DepartmentDropdownDto
    {
        /// <summary>
        /// Departmanın benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Departmanın adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Üst departmanın ID'si (null ise ana departman)
        /// </summary>
        public Guid? ParentDepartmentId { get; set; }
        
        /// <summary>
        /// Alt departmanların listesi (hiyerarşik yapı için)
        /// </summary>
        public List<DepartmentDropdownDto> SubDepartments { get; set; }
        
        /// <summary>
        /// DepartmentDropdownDto'nun yeni bir örneğini oluşturur
        /// </summary>
        public DepartmentDropdownDto()
        {
            SubDepartments = new List<DepartmentDropdownDto>();
        }
    }
} 