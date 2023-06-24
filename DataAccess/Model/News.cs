namespace DataAccess.Model
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity class for news 
    /// </summary>
    public class News
    {
        /// <summary>
        /// Newsid uniqly identify each news
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string? Id { get; set; }

        /// <summary>
        /// News title 
        /// </summary>

        public string? Title { get; set; }

        /// <summary>
        /// News Detail
        /// </summary>

        public string? Detail { get; set; }

        /// <summary>
        /// News image path
        /// </summary>

        public string? ImagePath { get; set; }

        /// <summary>
        /// Indicate that particular news is book marked or not 
        /// </summary>

        public bool? IsBookMark { get; set; }

        /// <summary>
        /// Date of particular news
        /// </summary>

        public DateTime? Date { get; set; }
    }
}
