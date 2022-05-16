using ArticleReview.Common.Business.Article;
using ArticleReview.Common.Dto.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ODataController
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<ArticleDto> Get()
        {
            return _articleService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ArticleDto?> Get(long id)
        {
            return await _articleService.ById(id);
        }

        [HttpPost]
        public async Task<AddArticleResDto> Post([FromBody] AddArticleDto dto)
        {
            return await _articleService.Insert(dto);
        }

        [HttpPut("{id}")]
        public async Task<UpdateArticleResDto> Put(long id, [FromBody] UpdateArticleDto dto)
        {
            return await _articleService.Update(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task<DeleteArticleResDto> Delete(long id)
        {
            return await _articleService.Delete(id);
        }
    }
}
