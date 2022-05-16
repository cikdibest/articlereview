using ArticleReview.Common.Data;
using ArticleReview.Common.Data.Entities;
using ArticleReview.Common.Dto.Review;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReview.Common.Business.Review
{
    public class ReviewService : IReviewService
    {
        private readonly ArticleReviewDbContext _dbContext;
        public ReviewService(ArticleReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ReviewDto> Get()
        {
            return _dbContext.Reviews
                .Select(s => new ReviewDto
                {
                    Id = s.Id,
                    ArticleId = s.ArticleId,
                    ReviewContent = s.ReviewContent,
                    Reviewer = s.Reviewer
                });
        }

        public async Task<ReviewDto> ById(long id)
        {
            return await _dbContext.Reviews.Where(q => q.Id == id)
                            .Select(s =>
                                  new ReviewDto
                                  {
                                      Id = s.Id,
                                      ArticleId = s.ArticleId,
                                      ReviewContent = s.ReviewContent,
                                      Reviewer = s.Reviewer
                                  }).FirstOrDefaultAsync();
        }

        public async Task<DeleteReviewResDto> Delete(long id)
        {
            var result = new DeleteReviewResDto { Success = false };

            var ent = await _dbContext.Reviews.SingleOrDefaultAsync(q => q.Id == id);
            if (ent is null)
                return result;

            _dbContext.Reviews.Remove(ent);
            result.Success = await _dbContext.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<AddReviewResDto> Insert(AddReviewDto dto)
        {
            var result = new AddReviewResDto { Success = false };

            if (dto.ArticleId < 1)
                return result;

            if (!await _dbContext.Articles.AnyAsync(a => a.Id == dto.ArticleId))
                return result;

            await _dbContext.Reviews.AddAsync(
                new ReviewEntity
                {
                    ArticleId = dto.ArticleId,
                    ReviewContent = dto.ReviewContent,
                    Reviewer = dto.Reviewer
                });
            result.Success = await _dbContext.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<UpdateReviewResDto> Update(long id, UpdateReviewDto dto)
        {
            var result = new UpdateReviewResDto { Success = false };

            if (dto.ArticleId < 1)
                return result;

            if (!await _dbContext.Articles.AnyAsync(a => a.Id == dto.ArticleId))
                return result;

            var ent = await _dbContext.Reviews.SingleOrDefaultAsync(q => q.Id == id);
            if (ent is null)
                return result;

            ent.ArticleId = dto.ArticleId;
            ent.ReviewContent = dto.ReviewContent;
            ent.Reviewer = dto.Reviewer;
            
            _dbContext.Reviews.Update(ent);
            result.Success = await _dbContext.SaveChangesAsync() > 0;

            return result;
        }
    }
}
