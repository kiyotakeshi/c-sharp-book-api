using System.Collections.Generic;
using System.Linq;
using BookApi.Dtos;
using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        private ICountryRepository _countryRepository;
        private IAuthorRepository _authorRepository;
        public CountriesController(ICountryRepository countryRepository, IAuthorRepository authorRepository)
        {
            _countryRepository = countryRepository;
            _authorRepository = authorRepository;
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
        [HttpGet("{countryId}", Name = "GetCountry")]
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
            if(!_authorRepository.AuthorExists(authorId))
                return NotFound();

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

        // /api/countires/countryId/authors/
        [HttpGet("{countryId}/authors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorsFromACountry(int countryId)
        {
            if(!_countryRepository.CountryExists(countryId))
                return NotFound();

            var authors = _countryRepository.GetAuthorsFromACountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorsDto = new List<AuthorDto>();

            foreach(var author in authors)
            {
                authorsDto.Add(new AuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                });
            }

            return Ok(authorsDto);
        }

        // api/countries
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Country))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCountry([FromBody] Country countryToCreate)
        {
            if(countryToCreate == null)
                return BadRequest();

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryToCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if(country != null)
            {
                ModelState.AddModelError("", $"Country {countryToCreate.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_countryRepository.CreateCountry(countryToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {countryToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            // routing to `api/countries/countryId`
            return CreatedAtRoute("GetCountry", new { countryId = countryToCreate.Id}, countryToCreate);
        }

        // 以下のエラーが解消しない(DB の table の設定を変更したが... `SET IDENTITY_INSERT Book.dbo.Countries ON;` )
        // inmemory でやる方法を考えるか、 mssql との繋ぎ込みに関しては一旦は深追いしない
        // Microsoft.Data.SqlClient.SqlException (0x80131904): Cannot insert explicit value for identity column in table 'Countries' when IDENTITY_INSERT is set to OFF.
        // [HttpPut("{countryId}")]
        // [ProducesResponseType(204)] // no content
        // [ProducesResponseType(400)]
        // [ProducesResponseType(404)]
        // [ProducesResponseType(422)]
        // [ProducesResponseType(500)]
        // public IActionResult UpdateCountry(int countryId, [FromBody] Country updateCountryInfo)
        // {
        //     if(updateCountryInfo == null)
        //         return BadRequest(ModelState);

        //     if(countryId != updateCountryInfo.Id)
        //         return BadRequest();

        //     if(!_countryRepository.CountryExists(countryId))
        //         return NotFound();

        //     // 重複していたらエラー
        //     if(_countryRepository.IsDuplicateCountryName(countryId, updateCountryInfo.Name))
        //     {
        //         ModelState.AddModelError("", $"Country {updateCountryInfo.Name} already exists");
        //         return StatusCode(422, ModelState);
        //     }

        //     if(!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     if(!_countryRepository.CreateCountry(updateCountryInfo))
        //     {
        //         ModelState.AddModelError("", $"Something went wrong updating {updateCountryInfo.Name}");
        //         return StatusCode(500, ModelState);
        //     }
        //     return NoContent();
        // }
    }
}
