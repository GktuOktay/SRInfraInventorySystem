using SRInfraInventorySystem.Core.Common;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Sunucularda bulunan veritabanlarını temsil eden entity.
    /// Veritabanı bilgileri, erişim izinleri ve ilişkili kaynakları içerir.
    /// </summary>
    public class Database : BaseEntity
    {
        /// <summary>
        /// Veritabanının adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Veritabanının tipi (SQL Server, MySQL, PostgreSQL vb.)
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Veritabanının versiyonu
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Veritabanından sorumlu kişi
        /// </summary>
        public string ResponsiblePerson { get; set; }
        
        /// <summary>
        /// Veritabanına erişim izinleri
        /// </summary>
        public string AccessPermissions { get; set; }
        
        /// <summary>
        /// Veritabanına bağlanmak için kullanılan araçlar
        /// </summary>
        public string ConnectionTools { get; set; }
        
        /// <summary>
        /// Veritabanı bağlantı dizesi
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        /// Veritabanı hakkında açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Veritabanının bulunduğu sunucunun ID'si
        /// </summary>
        public Guid ServerId { get; set; }
        
        /// <summary>
        /// Veritabanına bağlı uygulamanın ID'si (opsiyonel)
        /// </summary>
        public Guid? ApplicationId { get; set; }
        
        // Navigation Properties
        
        /// <summary>
        /// Veritabanının bulunduğu sunucu
        /// </summary>
        public Server Server { get; set; }
        
        /// <summary>
        /// Veritabanına bağlı uygulama
        /// </summary>
        public Application ConnectedApplication { get; set; }
        
        /// <summary>
        /// Veritabanına yapılan erişim loglarının koleksiyonu
        /// </summary>
        public ICollection<AccessLog> AccessLogs { get; set; }
        
        /// <summary>
        /// Database entity'sinin yeni bir örneğini oluşturur
        /// </summary>
        public Database()
        {
            AccessLogs = new HashSet<AccessLog>();
        }
    }
} 