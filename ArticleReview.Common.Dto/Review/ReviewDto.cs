using ArticleReview.Common.Dto.Base;

namespace ArticleReview.Common.Dto.Review
{
    public class ReviewDto : BaseDto
    {
        public long ArticleId { get; set; }
        public string Reviewer { get; set; }
        public string ReviewContent { get; set; }
    }
}
