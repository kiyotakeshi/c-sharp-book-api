using System.Collections.Generic;
using System.Linq;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        private ICountryRepository _countryRepository;
        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)] // 必須ではない記述、ドキュメント的な役割
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))] // 必須ではない記述、ドキュメント的な役割
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countriesDto = new List<CountryDto>();
            foreach (var country in countries)
            {
                countriesDto.Add(new CountryDto{
                    Id = country.Id,
                    Name = country.Name
                });
            }
            return Ok(countriesDto);
        }

        // api/countries/countryId
        [HttpGet("{countryId}")]
        [ProducesResponseType(400)] // 必須ではない記述、ドキュメント的な役割
        [ProducesResponseType(404)] // 必須ではない記述、ドキュメント的な役割
        [ProducesResponseType(200, Type = typeof(CountryDto))] // 必須ではない記述、ドキュメント的な役割
        public IActionResult GetCounty(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country = _countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            return Ok(countryDto);
        }

        // api/countries/authors/authorId
        [HttpGet("authors/{authorId}")]
        [ProducesResponseType(400)] // 必須ではない記述、ドキュメント的な役割
        [ProducesResponseType(404)] // 必須ではない記述、ドキュメント的な役割
        [ProducesResponseType(200, Type = typeof(CountryDto))] // 必須ではない記述、ドキュメント的な役割
        public IActionResult GetCountyOfAnAuthor(int authorId)
        {
            // TODO: Validate the author exists
            var country = _countryRepository.GetCountryOfAnAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            return Ok(countryDto);
        }

        // TODO: GetAuthorsFromCountry
    }
}
