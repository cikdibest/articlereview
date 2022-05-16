using ArticleReview.Common.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArticleReview.Common.Data.Entities
{
    public class ArticleEntity : BaseEntity
    {
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string? Author { get; set; }
        public string? ArticleContent { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? StarCount { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }
    }
}
