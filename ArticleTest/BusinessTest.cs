using ArticleReview.Common.Business.Article;
using ArticleReview.Common.Data;
using ArticleReview.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ArticleTest
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
        public async Task Should_CheckTheExistenceOfTheArticle_When_DeletingAnArticle()
        {
            var dbContext = new ArticleReviewDbContext(dbContextOptions);
            var articleService = new ArticleService(dbContext);

            //act
            var result = await articleService.Delete(1);

            // Assert
            Assert.Equal(false, result.Success);
        }

        [Fact]
        public async Task Deletion_Article_Successful()
        {
            var dbContext = new ArticleReviewDbContext(dbContextOptions);
            dbContext.Articles.Add(new ArticleEntity { Title = "test", ArticleContent = "test", Author = "test", PublishDate = DateTime.Now, StarCount = 5 });
            await dbContext.SaveChangesAsync();
            var articleService = new ArticleService(dbContext);

            //act
            var result = await articleService.Delete(1);
            var data = await articleService.ById(1);

            // Assert
            Assert.Equal(true, result.Success);
            Assert.Null(data);
        }
    }
}
