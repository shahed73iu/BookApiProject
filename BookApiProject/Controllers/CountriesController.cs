﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Dtos;
using BookApiProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApiProject.Controllers
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

        //api/countries
        [HttpGet]
        [ProducesResponseType(200 , Type = typeof(IEnumerable<CountryDto>))]
       // [ProducesResponseType(400)]
        public IActionResult GetCountries()
        {
             var countries =  _countryRepository.GetCountries().ToList();
            //var countries = _countryRepository.GetCountries().Where(c => c.Id == 250);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var countriesDto = new List<CountryDto>();
            foreach (var country in countries)
            {
                countriesDto.Add(new CountryDto 
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }
            return Ok(countriesDto);
        }

        //api/countries/countryId
        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetCountry(int countryId)
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




         
        //api/countries/authors/authorId
        [HttpGet("authors/{authorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetCountryOfAnAuthor(int authorId)
        {
            //validate the author exists
            var country = _countryRepository.GetCountryOfAnAuthor(authorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = new CountryDto()
            {
                Id=  country.Id,
                Name = country.Name
            };
            return Ok(countryDto);

            //To-do = //GetAuthorsFromACountry
        }
    }
}
