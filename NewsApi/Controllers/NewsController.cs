namespace NewsApi.Controllers
{
    using BusinessLogic.AppConstant;
    using BusinessLogic.IService;
    using DataAccess.Dto;
    using FluentValidation;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        #region Fields Declarations
        private readonly INewsService newsService;
        private readonly IValidator<NewsDto> newsValidator;
        private readonly IHostingEnvironment hostingEnvironment;
        #endregion

        #region Constructor
        public NewsController(INewsService newsService, IValidator<NewsDto> newsValidator)
        {
            this.newsService = newsService;
            this.newsValidator = newsValidator;
        }
        #endregion

        #region News Apis
        /// <summary>
        /// api for fetch all news
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<NewsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IResult> FetchAllNews()
        {         
            var newsList = await this.newsService.FetchAllNews();
            return newsList.Count == 0 ? Results.NoContent() : Results.Ok(newsList);
        }

        /// <summary>
        /// api for fetch all bookmarked news
        /// </summary>
        /// <param name="bookMark"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("FetchAllBookmarkedNews")]
        [ProducesResponseType(typeof(List<NewsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IResult> FetchAllBookmarkedNews(bool bookMark)
        {
            var bookmarkednewsList = await this.newsService.FetchAllBookmarkedNews(bookMark);
            return bookmarkednewsList.Count == 0 ? Results.NoContent() : Results.Ok(bookmarkednewsList);
        }


        [Route("SearchAllNews")]
        [HttpGet]
        [ProducesResponseType(typeof(List<NewsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IResult> SearchAllNews(string searchText,bool bookMarkSearch)
        {
            var newsList = await this.newsService.SearchInAllNews(searchText, bookMarkSearch);
            return newsList.Count == 0 ? Results.NoContent() : Results.Ok(newsList);
        }

        [Route("FetchNewsByNewsId")]
        [HttpGet]
        [ProducesResponseType(typeof(NewsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IResult> FetchNewsByNewsId(string newsId)
        {
            var news = await this.newsService.FetchNewsByNewsId(newsId);
            return news == null ? Results.NoContent() : Results.Ok(news);
        }


        [Route("BookMarkNews")]
        [HttpGet]
        [ProducesResponseType(typeof(NewsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> BookMarkNews(string newsId)
        {
            var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            var isBookMark = await this.newsService.BookMarkNews(newsId);
            return isBookMark ? Results.Ok(ResponseHelper.Success(isBookMark)) : Results.Problem(detail: "News not found.", title: "News not found.");
        }


        [Route("SaveNews")]
        [HttpPost]
        [ProducesResponseType(typeof(NewsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IResult Post([FromForm] NewsDto newsDto)
        {
            try
            {                   
                var validationResult = this.newsValidator.Validate(newsDto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(error => new { ErrorCode = error.ErrorCode, ErrorMessage = error.ErrorMessage });
                    return Results.BadRequest(ResponseHelper.Error(ApplicationHelper.ExceptionMsg, errors));
                }
                if (Request.Form.Files.Count >= 1)
                {
                    var fileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
                    var newsImage = Request.Form.Files.Where(file => file.Name == nameof(newsDto.NewsImage)).FirstOrDefault();
                    if (newsImage != null)
                    {
                        //Validate image extension
                        var newsImageExtension = Path.GetExtension(newsImage.FileName);
                        if (!(fileExtensions.Contains(newsImageExtension.ToLower())))
                        {
                            return Results.BadRequest(ResponseHelper.Error(ApplicationHelper.ImageValidationMsg, new object()));
                        }
                        //Validate image name lenth accept only lessthan 150 char in file name
                        if (newsImage.FileName.Length > ApplicationHelper.ImageNameLenth)
                        {
                            return Results.BadRequest(ResponseHelper.Error(ApplicationHelper.ImageNameLenthMsg, new object()));
                        }

                        string newsFilePath = ApplicationHelper.FilePath;
                        if (!Directory.Exists(newsFilePath))
                        {
                            Directory.CreateDirectory(newsFilePath);
                        }
                     
                        newsFilePath = Path.Combine(newsFilePath, newsImage.FileName);
                        using (var stream = new FileStream(newsFilePath, FileMode.Create))
                        {
                            newsDto.NewsImage.CopyTo(stream);
                        }
                        newsDto.ImagePath = string.Concat("~NewsImages/", newsImage.FileName);
                        newsDto.Id = System.Guid.NewGuid().ToString();
                        newsDto.Date = DateTime.UtcNow.Date;
                        newsDto.Provider = newsDto.Provider.ToUpper();
                        this.newsService.SaveNews(newsDto);
                    }
                }
                return Results.Ok(ResponseHelper.Success(ApplicationHelper.NewsSaveSuccess + "Id= " + newsDto.Id));
            }
            catch (Exception)
            {
                var exceptionHandlerFeature =
                 HttpContext.Features.Get<IExceptionHandlerFeature>()!;
                return Results.Problem(detail: exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
            }

        }

        #endregion


    }
}
