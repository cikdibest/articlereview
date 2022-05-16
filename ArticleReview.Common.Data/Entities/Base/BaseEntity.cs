using System.ComponentModel.DataAnnotations;

namespace ArticleReview.Common.Data.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
