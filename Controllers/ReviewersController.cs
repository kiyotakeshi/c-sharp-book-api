using System.Collections.Generic;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewersController: Controller
    {
        private IReviewerRepository _reviewerRepository;

        public ReviewersController(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetRevieweres()
        {
            var reviewers = _reviewerRepository.GetReviewers();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewersDto = new List<ReviewerDto>();

            foreach (var reviwer in reviewers)
            {
                reviewersDto.Add(new ReviewerDto
                {
                    Id = reviwer.Id,
                    FirstName = reviwer.FirstName,
                    LastName = reviwer.LastName
                });
            }
            return Ok(reviewersDto);
        }
    }
}
