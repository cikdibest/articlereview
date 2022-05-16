using ArticleReview.Common.Dto.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleReview.Common.Business.Article
{
    public interface IArticleService
    {
        IQueryable<ArticleDto> Get();
        Task<ArticleDto> ById(long id);
        Task<AddArticleResDto> Insert(AddArticleDto dto);
        Task<DeleteArticleResDto> Delete(long id);
        Task<UpdateArticleResDto> Update(long id, UpdateArticleDto dto);
    }
}
