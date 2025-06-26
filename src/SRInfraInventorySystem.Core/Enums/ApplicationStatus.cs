namespace SRInfraInventorySystem.Core.Enums
{
    /// <summary>
    /// Uygulama durumlarını tanımlayan enum
    /// </summary>
    public enum ApplicationStatus
    {
        /// <summary>
        /// Uygulama aktif ve çalışır durumda
        /// </summary>
        Active = 1,
        
        /// <summary>
        /// Uygulama pasif durumda
        /// </summary>
        Inactive = 2,
        
        /// <summary>
        /// Uygulama bakım modunda
        /// </summary>
        Maintenance = 3,
        Deprecated = 4
    }
} 