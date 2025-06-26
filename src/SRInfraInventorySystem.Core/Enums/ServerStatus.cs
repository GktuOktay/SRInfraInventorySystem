namespace SRInfraInventorySystem.Core.Enums
{
    /// <summary>
    /// Sunucu durumlarını tanımlayan enum
    /// </summary>
    public enum ServerStatus
    {
        /// <summary>
        /// Sunucu aktif ve çalışır durumda
        /// </summary>
        Active = 1,
        
        /// <summary>
        /// Sunucu pasif durumda
        /// </summary>
        Inactive = 2,
        
        /// <summary>
        /// Sunucu bakım modunda
        /// </summary>
        Maintenance = 3,
        
        /// <summary>
        /// Sunucu hizmet dışı bırakılmış
        /// </summary>
        Decommissioned = 4
    }
} 