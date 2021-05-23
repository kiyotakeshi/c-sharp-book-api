using System.Collections.Generic;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        public ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)] // 必須ではない記述、ドキュメント的な役割
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))] // 必須ではない記述、ドキュメント的な役割
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto{
                    Id = category.Id,
                    Name = category.Name
                });
            }
            return Ok(categoriesDto);
        }
    }
}
