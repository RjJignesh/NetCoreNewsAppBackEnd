namespace BusinessLogic.Services
{
    using AutoMapper;
    using BusinessLogic.IService;
    using DataAccess;
    using DataAccess.Dto;
    using DataAccess.Model;
    using Microsoft.EntityFrameworkCore;
    public class NewsService : INewsService
    {
        #region Fields Declaration
        /// <summary>
        /// read only field for intialise data base context
        /// </summary>
        private readonly NewsDbContext newsDbContext;
        /// <summary>
        ///  read only field for automapper configuration
        /// </summary>
        private readonly IMapper newsMapper;
        #endregion


        #region  Constructor
        /// <summary>
        /// constructor of employee questionnaire mapping service
        /// </summary>
        /// <param name="roboSoftPlociyContext"></param>
        /// <param name="robosoftMapper"></param>
        public NewsService(NewsDbContext newsDbContext, IMapper newsMapper)
        {
            this.newsDbContext = newsDbContext;
            this.newsMapper = newsMapper;
        }

        #endregion

        /// <summary>
        /// Fetch all news and top news. 
        /// </summary>
        /// <returns></returns>
        public async Task<List<NewsDto>> FetchAllNews()
        {
            var allNews = await this.newsDbContext.News.OrderByDescending(news => news.Date).ToListAsync();
            var allNewsDtos = this.newsMapper.Map<List<News>, List<NewsDto>>(allNews);
            return allNewsDtos;
        }


        /// <summary>
        /// Fetch all news. 
        /// </summary>
        /// <returns></returns>
        public async Task<List<NewsDto>> SearchInAllNews(string searchText)
        {
            var allNews = await (this.newsDbContext.News.Where(news => news.Title.ToLower().Contains            
            (searchText.ToLower()) || news.Detail.ToLower().Contains(searchText.ToLower()))).OrderBy
            (news => news.Title).ThenBy(news => news.Detail).AsQueryable().ToListAsync();
            var allNewsDtos = this.newsMapper.Map<List<News>, List<NewsDto>>(allNews);
            return allNewsDtos;
        }

        /// <summary>
        /// fetch news base on particular newsid.
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<NewsDto> FetchNewsByNewsId(string Id)
        {
            var news = await this.newsDbContext.News.Where(n => n.Id == Id).FirstOrDefaultAsync();
            if (news != null)
            {
                return this.newsMapper.Map<News, NewsDto>(news);
            }
            else
            {
                return null;
            }

        }



        /// <summary>
        ///  BookMark particular news 
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<bool> BookMarkNews(string Id)
        {
            var news = this.newsDbContext.News.Where(news => news.Id == Id).FirstOrDefault();
            if (news != null)
            {
                news.IsBookMark = true;
                this.newsDbContext.Update(news);
                await this.newsDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Insert new entry in news table.
        /// </summary>
        /// <param name="newsDto"></param>
        /// <returns></returns>
        public async Task<string> SaveNews(NewsDto newsDto)
        {
            var news = this.newsMapper.Map<News>(newsDto);
            await this.newsDbContext.AddAsync(news);
            this.newsDbContext.SaveChanges();
            return news.Id ?? string.Empty;
        }
    }
}
