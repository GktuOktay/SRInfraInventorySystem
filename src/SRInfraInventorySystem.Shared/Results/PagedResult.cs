using System.Collections.Generic;

namespace SRInfraInventorySystem.Shared.Results
{
    /// <summary>
    /// Sayfalanmış veri sonuçları için kullanılan sınıf.
    /// Toplam kayıt sayısı, sayfa bilgileri ve veriyi içerir.
    /// </summary>
    /// <typeparam name="T">Sayfalanacak veri tipi</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Sayfalanmış veri listesi
        /// </summary>
        public IEnumerable<T> Data { get; set; }
        
        /// <summary>
        /// Toplam kayıt sayısı
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// Toplam sayfa sayısı
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Mevcut sayfa numarası
        /// </summary>
        public int CurrentPage { get; set; }
        
        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Mevcut sayfadaki kayıt sayısı
        /// </summary>
        public int CurrentPageSize { get; set; }
        
        /// <summary>
        /// Önceki sayfa var mı?
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;
        
        /// <summary>
        /// Sonraki sayfa var mı?
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;
        
        /// <summary>
        /// İlk sayfa numarası
        /// </summary>
        public int FirstPage => 1;
        
        /// <summary>
        /// Son sayfa numarası
        /// </summary>
        public int LastPage => TotalPages;
        
        /// <summary>
        /// PagedResult'ın yeni bir örneğini oluşturur
        /// </summary>
        public PagedResult()
        {
            Data = new List<T>();
        }
        
        /// <summary>
        /// PagedResult'ın yeni bir örneğini oluşturur
        /// </summary>
        /// <param name="data">Sayfalanmış veri</param>
        /// <param name="totalCount">Toplam kayıt sayısı</param>
        /// <param name="currentPage">Mevcut sayfa</param>
        /// <param name="pageSize">Sayfa boyutu</param>
        public PagedResult(IEnumerable<T> data, int totalCount, int currentPage, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            CurrentPageSize = data is ICollection<T> collection ? collection.Count : 0;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        }
        
        /// <summary>
        /// Boş sayfa sonucu oluşturur
        /// </summary>
        /// <param name="currentPage">Mevcut sayfa</param>
        /// <param name="pageSize">Sayfa boyutu</param>
        /// <returns>Boş PagedResult</returns>
        public static PagedResult<T> Empty(int currentPage = 1, int pageSize = 10)
        {
            return new PagedResult<T>
            {
                Data = new List<T>(),
                TotalCount = 0,
                CurrentPage = currentPage,
                PageSize = pageSize,
                CurrentPageSize = 0,
                TotalPages = 0
            };
        }
    }
} 