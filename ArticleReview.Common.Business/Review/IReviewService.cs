using ArticleReview.Common.Dto.Review;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReview.Common.Business.Review
{
    public interface IReviewService
    {
        IQueryable<ReviewDto> Get();
        Task<ReviewDto> ById(long id);
        Task<AddReviewResDto> Insert(AddReviewDto dto);
        Task<DeleteReviewResDto> Delete(long id);
        Task<UpdateReviewResDto> Update(long id, UpdateReviewDto dto);
    }
}
