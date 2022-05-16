
namespace ArticleReview.Common.Dto.Review
{
    public class UpdateReviewDto
    {
        public long ArticleId { get; set; }
        public string Reviewer { get; set; }
        public string ReviewContent { get; set; }
    }
}
