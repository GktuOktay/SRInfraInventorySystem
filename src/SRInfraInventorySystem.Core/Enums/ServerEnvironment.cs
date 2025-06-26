namespace SRInfraInventorySystem.Core.Enums
{
    /// <summary>
    /// Sunucu çalışma ortamlarını tanımlayan enum
    /// </summary>
    public enum ServerEnvironment
    {
        /// <summary>
        /// Geliştirme ortamı
        /// </summary>
        Development = 1,
        
        /// <summary>
        /// Test ortamı
        /// </summary>
        Test = 2,
        
        /// <summary>
        /// Canlı ortam
        /// </summary>
        Staging = 3,
        
        /// <summary>
        /// Canlı ortam
        /// </summary>
        Production = 4
    }
} 