using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /// <summary>
    /// Application db context class 
    /// </summary>
    public class NewsDbContext: DbContext
    {
        /// <summary>
        /// constructor 
        /// </summary>
        public NewsDbContext()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// virtual property for map sql table to entity framwork entity class
        /// </summary>
        public virtual DbSet<News> News { get; set; }

        /// <summary>
        /// method on onconfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseCosmos("https://3e32106b-0ee0-4-231-b9ee.documents.azure.com:443/", "yPpx7pNzEqCu19zV1EgH8f9N7SrZkTcckgW3OM6ikD4Mrs6vItl0ggQPlXDpSaMBifNS3XOpwvcCACDbZ0Mzug==",
                       "NewsDb");
            }
        }

        /// <summary>
        /// method onmodelcreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().ToContainer("News").HasPartitionKey(news=>news.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}