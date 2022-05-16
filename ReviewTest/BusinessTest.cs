using ArticleReview.Common.Business.Review;
using ArticleReview.Common.Data;
using ArticleReview.Common.Data.Entities;
using ArticleReview.Common.Dto.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ReviewTest
{
    public class BusinessTest
    {
        private DbContextOptions<ArticleReviewDbContext> dbContextOptions;

        public BusinessTest()
        {
            var dbName = $"ArticleReviewDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<ArticleReviewDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task Should_CheckExistenceOfTheArticleProvided_When_CreatingAReview()
        {
            var dbContext = new ArticleReviewDbContext(dbContextOptions);
            var reviewService = new ReviewService(dbContext);

            //act
            var result = await reviewService.Insert(new AddReviewDto { ArticleId = 1, ReviewContent = "test", Reviewer = "test" });

            // Assert
            Assert.Equal(false, result.Success);
        }

        [Fact]
        public async Task Creating_Review_Successful()
        {
            var dbContext = new ArticleReviewDbContext(dbContextOptions);
            dbContext.Articles.Add(new ArticleEntity { Title = "test", ArticleContent = "test", Author = "test", PublishDate = DateTime.Now, StarCount = 5 });
            await dbContext.SaveChangesAsync();
            var reviewService = new ReviewService(dbContext);

            //act
            var result = await reviewService.Insert(new AddReviewDto { ArticleId = 1, ReviewContent = "test", Reviewer = "test" });

            // Assert
            Assert.Equal(true, result.Success);
        }
    }
}
