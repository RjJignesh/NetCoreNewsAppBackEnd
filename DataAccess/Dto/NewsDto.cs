namespace DataAccess.Dto
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// NewsDto class for mappling News entity clss
    /// </summary>
    [ValidateNever]
    public class NewsDto
    {
        /// <summary>
        /// Newsid uniqly identify each news
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// News title 
        /// </summary>       
        public string Title { get; set; }

        /// <summary>
        /// News Detail
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// News image path for display image on front end
        /// </summary>   
        public string? ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// Indicate that particular news is book marked or not 
        /// </summary>
        public bool IsBookMark { get; set; }

        /// <summary>
        /// Date of particular news
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Filed for hold actual image data
        /// </summary>
        [Required(ErrorMessage = "Please provide NewsImage")]
        public IFormFile NewsImage { get; set; }     
    }
}
