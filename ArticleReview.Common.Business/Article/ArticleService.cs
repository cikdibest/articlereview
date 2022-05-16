using ArticleReview.Common.Data;
using ArticleReview.Common.Data.Entities;
using ArticleReview.Common.Dto.Article;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReview.Common.Business.Article
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleReviewDbContext _dbContext;
        public ArticleService(ArticleReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<ArticleDto> Get()
        {
            return _dbContext.Articles
                .Select(s => new ArticleDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArticleContent = s.ArticleContent,
                    Author = s.Author,
                    PublishDate = s.PublishDate,
                    StarCount = s.StarCount
                });
        }

        public async Task<ArticleDto> ById(long id)
        {
            return await _dbContext.Articles.Where(q => q.Id == id)
                            .Select(s =>
                                  new ArticleDto
                                  {
                                      Id = s.Id,
                                      Title = s.Title,
                                      ArticleContent = s.ArticleContent,
                                      Author = s.Author,
                                      PublishDate = s.PublishDate,
                                      StarCount = s.StarCount
                                  }).FirstOrDefaultAsync();
        }

        public async Task<AddArticleResDto> Insert(AddArticleDto dto)
        {
            await _dbContext.Articles.AddAsync(
                new ArticleEntity
                {
                    Title = dto.Title,
                    ArticleContent = dto.ArticleContent,
                    Author = dto.Author,
                    PublishDate = dto.PublishDate,
                    StarCount = dto.StarCount
                });
            var result = await _dbContext.SaveChangesAsync() > 0;
            return new AddArticleResDto { Success = result };
        }

        public async Task<DeleteArticleResDto> Delete(long id)
        {
            var result = new DeleteArticleResDto { Success = false };

            if (await _dbContext.Reviews.AnyAsync(a => a.ArticleId == id))
                return result;

            var ent = await _dbContext.Articles.SingleOrDefaultAsync(q => q.Id == id);
            if(ent is null)
                return result;

             _dbContext.Articles.Remove(ent);
            result.Success = await _dbContext.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<UpdateArticleResDto> Update(long id, UpdateArticleDto dto)
        {
            var result = new UpdateArticleResDto { Success = false };

            var ent = await _dbContext.Articles.SingleOrDefaultAsync(q => q.Id == id);
            if (ent is null)
                return result;

            ent.Title = dto.Title;
            ent.ArticleContent = dto.ArticleContent;
            ent.Author = dto.Author;
            ent.PublishDate = dto.PublishDate;
            ent.StarCount = dto.StarCount;

            _dbContext.Articles.Update(ent);
            result.Success = await _dbContext.SaveChangesAsync() > 0;

            return result;
        }
    }
}
