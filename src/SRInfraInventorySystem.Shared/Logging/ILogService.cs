using System;

namespace SRInfraInventorySystem.Shared.Logging
{
    /// <summary>
    /// Uygulama genelinde loglama işlemleri için interface.
    /// Farklı log seviyelerini destekler.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Debug seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        void Debug(string message);
        
        /// <summary>
        /// Info seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        void Info(string message);
        
        /// <summary>
        /// Warning seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        void Warning(string message);
        
        /// <summary>
        /// Error seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="exception">Hata detayı (opsiyonel)</param>
        void Error(string message, Exception exception = null);
        
        /// <summary>
        /// Fatal seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="exception">Hata detayı (opsiyonel)</param>
        void Fatal(string message, Exception exception = null);

        void LogUserActivity(string userName, string action, string details);
    }
} 