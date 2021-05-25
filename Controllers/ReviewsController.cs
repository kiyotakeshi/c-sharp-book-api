using System.Collections.Generic;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController: Controller
    {
        private IReviewRepository _reviewRepository;
        private IReviewerRepository _reviewerRepository;
        private IBookRepository _bookRepository;
        public ReviewsController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository,IBookRepository bookRepository
            )
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewsDto = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return Ok(reviewsDto);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(reviewId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Headline = review.Headline,
                Rating = review.Rating,
                ReviewText = review.ReviewText
            };

            return Ok(reviewDto);
        }

        // api/reviews/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsForABook(int bookId)
        {
            if(!_bookRepository.BookExists(bookId))
                return NotFound();

            var reviews = _reviewRepository.GetReviewsOfABook(bookId);

            if(!_reviewerRepository.ReviewerExists(bookId))
                return NotFound();

            var reviewsDto = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto()
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            return Ok(reviewsDto);
        }

        // api/reviews/reviewId/book
        [HttpGet("{reviewId}/book")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetBookOfAReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var book = _reviewRepository.GetBookOfAReview(reviewId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Isbn = book.Isbn,
                DatePublished = book.DatePublished
            };
            return Ok(bookDto);
        }

    }
}
