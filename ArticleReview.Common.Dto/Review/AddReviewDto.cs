
namespace ArticleReview.Common.Dto.Review
{
    public class AddReviewDto
    {
        public long ArticleId { get; set; }
        public string Reviewer { get; set; }
        public string ReviewContent { get; set; }
    }
}
