using System;
using System.Collections.Generic;
using System.Linq;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentDepartmentId { get; set; }
}

public class SortingTest
{
    public static void Main()
    {
        // JSON verinizdeki departmanları aynı şekilde oluşturuyorum
        var departments = new List<DepartmentDto>
        {
            new DepartmentDto
            {
                Id = Guid.Parse("81b840d7-d6ac-4b77-c235-08ddb3a872ef"),
                Name = "İSBAK Genel Müdürlüğü",
                ParentDepartmentId = null
            },
            new DepartmentDto
            {
                Id = Guid.Parse("73469a3f-0faf-4a47-c236-08ddb3a872ef"),
                Name = "Bilgi İşlem Müdürlüğü",
                ParentDepartmentId = null
            },
            new DepartmentDto
            {
                Id = Guid.Parse("2e72ce32-d4ff-4499-c237-08ddb3a872ef"),
                Name = "Bilgi Sistemleri Şefliği",
                ParentDepartmentId = Guid.Parse("73469a3f-0faf-4a47-c236-08ddb3a872ef")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("efd71045-fea5-4c13-c238-08ddb3a872ef"),
                Name = "Proje Yönetim Şefliği",
                ParentDepartmentId = Guid.Parse("73469a3f-0faf-4a47-c236-08ddb3a872ef")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("839911bf-c9db-44c4-298d-08ddb3bffad1"),
                Name = "Bilgi Teknolojileri ve Ar-Ge Genel Müdür Yardımcılığı",
                ParentDepartmentId = Guid.Parse("81b840d7-d6ac-4b77-c235-08ddb3a872ef")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("5db567aa-a813-4dd6-298e-08ddb3bffad1"),
                Name = "Ar-Ge Müdürlüğü",
                ParentDepartmentId = Guid.Parse("839911bf-c9db-44c4-298d-08ddb3bffad1")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("88d789d0-1e29-4877-298f-08ddb3bffad1"),
                Name = "Yazılım Geliştirme Şefliği",
                ParentDepartmentId = Guid.Parse("5db567aa-a813-4dd6-298e-08ddb3bffad1")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("81d87a3b-3305-4900-2990-08ddb3bffad1"),
                Name = "Akıllı Şehircilik ve Coğrafi Bilgi Sistemleri Şefliği",
                ParentDepartmentId = Guid.Parse("5db567aa-a813-4dd6-298e-08ddb3bffad1")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("4060ff6d-b26c-4bb7-2991-08ddb3bffad1"),
                Name = "Elektronik Sistemler Tasarım Şefliği",
                ParentDepartmentId = Guid.Parse("5db567aa-a813-4dd6-298e-08ddb3bffad1")
            },
            new DepartmentDto
            {
                Id = Guid.Parse("4e7e8a34-1c50-4a6e-2992-08ddb3bffad1"),
                Name = "Ar-Ge Projeleri Yönetim Şefliği",
                ParentDepartmentId = Guid.Parse("5db567aa-a813-4dd6-298e-08ddb3bffad1")
            }
        };

        Console.WriteLine("Sıralama öncesi:");
        foreach (var dept in departments)
        {
            Console.WriteLine($"- {dept.Name}");
        }

        // FlattenDepartments algoritması
        var sorted = FlattenDepartments(departments);

        Console.WriteLine("\nSıralama sonrası:");
        foreach (var dept in sorted)
        {
            Console.WriteLine($"- {dept.Name}");
        }

        Console.WriteLine("\nAr-Ge Müdürlüğü altındaki şeflikler:");
        var argeId = Guid.Parse("5db567aa-a813-4dd6-298e-08ddb3bffad1");
        var argeSubDepts = departments.Where(d => d.ParentDepartmentId == argeId)
                                    .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
                                    .ToList();
        
        foreach (var dept in argeSubDepts)
        {
            Console.WriteLine($"- {dept.Name}");
        }
    }

    private static List<DepartmentDto> FlattenDepartments(IEnumerable<DepartmentDto> departments, Guid? parentId = null)
    {
        var children = departments
            .Where(d => d.ParentDepartmentId == parentId)
            .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        var result = new List<DepartmentDto>();
        foreach (var child in children)
        {
            result.Add(child);
            result.AddRange(FlattenDepartments(departments, child.Id));
        }
        return result;
    }
} 