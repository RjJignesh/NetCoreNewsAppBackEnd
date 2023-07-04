namespace BusinessLogic.IService
{
    using DataAccess.Dto;

    /// <summary>
    /// Interface for News related methods
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        ///Fetch all news from news table
        /// </summary>
        /// <returns></returns>
        Task<List<NewsDto>> FetchAllNews();

        /// <summary>
        ///Fetch all Bookmarked news table
        /// </summary>
        /// <returns></returns>
        Task<List<NewsDto>> FetchAllBookmarkedNews(bool bookMark);

        /// <summary>
        /// Fetch all news. 
        /// </summary>
        /// <returns></returns>
        Task<List<NewsDto>> SearchInAllNews(string searchText, bool bookMarkSearch);


        /// <summary>
        /// Fetch particular news from news table base on single news id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<NewsDto> FetchNewsByNewsId(string Id);

        /// <summary>
        /// BookMark particular news 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> BookMarkNews(string Id);

        /// <summary>
        /// Insert new entry in news table.
        /// </summary>
        /// <param name="newsDto"></param>
        /// <returns></returns>
        Task<string> SaveNews(NewsDto newsDto);
    }
}
