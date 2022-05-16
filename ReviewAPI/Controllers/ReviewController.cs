using ArticleReview.Common.Business.Review;
using ArticleReview.Common.Dto.Review;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ODataController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<ReviewDto> Get()
        {
            return _reviewService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ReviewDto?> Get(long id)
        {
            return await _reviewService.ById(id);
        }

        [HttpPost]
        public async Task<AddReviewResDto> Post([FromBody] AddReviewDto dto)
        {
            return await _reviewService.Insert(dto);
        }

        [HttpPut("{id}")]
        public async Task<UpdateReviewResDto> Put(long id, [FromBody] UpdateReviewDto dto)
        {
            return await _reviewService.Update(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task<DeleteReviewResDto> Delete(long id)
        {
            return await _reviewService.Delete(id);
        }
    }
}
