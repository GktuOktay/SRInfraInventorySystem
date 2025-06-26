using System.Collections.Generic;

namespace SRInfraInventorySystem.Shared.Results
{
    /// <summary>
    /// API yanıtları için standart sonuç sınıfı.
    /// Başarı/hata durumunu ve veriyi içerir.
    /// </summary>
    /// <typeparam name="T">Döndürülecek veri tipi</typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// İşlemin başarılı olup olmadığını belirten flag
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Success property'sinin alternatif adı (geriye uyumluluk için)
        /// </summary>
        public bool IsSuccess => Success;
        
        /// <summary>
        /// İşlem sonucu hakkında mesaj
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// İşlem sonucunda döndürülecek veri
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Hata durumunda detay hata mesajlarının listesi
        /// </summary>
        public List<string> Errors { get; set; }
        
        /// <summary>
        /// ApiResult'ın yeni bir örneğini oluşturur
        /// </summary>
        public ApiResult()
        {
            Errors = new List<string>();
        }
        
        /// <summary>
        /// Başarılı sonuç oluşturur
        /// </summary>
        /// <param name="data">Döndürülecek veri</param>
        /// <param name="message">Başarı mesajı (opsiyonel)</param>
        /// <returns>Başarılı ApiResult</returns>
        public static ApiResult<T> SuccessResult(T data, string message = null)
        {
            return new ApiResult<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }
        
        /// <summary>
        /// Hatalı sonuç oluşturur
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="errors">Detay hata listesi (opsiyonel)</param>
        /// <returns>Hatalı ApiResult</returns>
        public static ApiResult<T> ErrorResult(string message, List<string> errors = null)
        {
            return new ApiResult<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
} 