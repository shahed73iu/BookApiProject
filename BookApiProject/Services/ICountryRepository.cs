﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Models;

namespace BookApiProject.Services
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int countryId);
        Country GetCountryOfAnAuthor(int authorId);
       ICollection<Author> GetAuthorsFromACountry(int countryId);
        bool CountryExists(int countryId);

    }
}
