using ArticleReview.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleReview.Common.Data
{
    public class ArticleReviewDbContext : DbContext
    {
        public ArticleReviewDbContext(DbContextOptions<ArticleReviewDbContext> options) : base(options) { }

        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
    }
}
