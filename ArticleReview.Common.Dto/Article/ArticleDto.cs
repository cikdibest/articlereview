using ArticleReview.Common.Dto.Base;
using System;

namespace ArticleReview.Common.Dto.Article
{
    public class ArticleDto : BaseDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ArticleContent { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? StarCount { get; set; }
    }
}
