using ArticleReview.Common.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticleReview.Common.Data.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public long ArticleId { get; set; }
        [MaxLength(200)]
        public string Reviewer { get; set; }
        public string ReviewContent { get; set; }

        [ForeignKey("ArticleId")]
        public ArticleEntity Article { get; set; }
    }
}
