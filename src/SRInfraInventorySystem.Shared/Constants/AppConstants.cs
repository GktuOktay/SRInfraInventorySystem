namespace SRInfraInventorySystem.Shared.Constants
{
    /// <summary>
    /// Uygulama genelinde kullanılan sabitler.
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// Varsayılan sayfa boyutu
        /// </summary>
        public const int DefaultPageSize = 10;
        
        /// <summary>
        /// Maksimum sayfa boyutu
        /// </summary>
        public const int MaxPageSize = 100;
        
        /// <summary>
        /// Minimum sayfa boyutu
        /// </summary>
        public const int MinPageSize = 1;
        
        /// <summary>
        /// Varsayılan sayfa numarası
        /// </summary>
        public const int DefaultPageNumber = 1;
        
        /// <summary>
        /// Minimum sayfa numarası
        /// </summary>
        public const int MinPageNumber = 1;
        
        /// <summary>
        /// API versiyonu
        /// </summary>
        public const string ApiVersion = "1.0.0";
        
        /// <summary>
        /// Uygulama adı
        /// </summary>
        public const string ApplicationName = "SR Infrastructure Inventory System";
        
        /// <summary>
        /// Uygulama açıklaması
        /// </summary>
        public const string ApplicationDescription = "SR Altyapı Envanter Yönetim Sistemi";
        
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string BTPersonel = "BTPersonel";
            public const string Viewer = "Viewer";
        }
        
        public static class Claims
        {
            public const string UserId = "UserId";
            public const string UserName = "UserName";
            public const string Role = "Role";
        }
        
        public static class Messages
        {
            public const string RecordNotFound = "Kayıt bulunamadı.";
            public const string RecordCreated = "Kayıt başarıyla oluşturuldu.";
            public const string RecordUpdated = "Kayıt başarıyla güncellendi.";
            public const string RecordDeleted = "Kayıt başarıyla silindi.";
            public const string Unauthorized = "Bu işlem için yetkiniz bulunmamaktadır.";
        }
    }
} 