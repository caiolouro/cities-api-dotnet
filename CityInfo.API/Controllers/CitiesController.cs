using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")] // Global route
    public class CitiesController : ControllerBase
    {
        public CitiesController()
        {
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);

            if (existingCity == null)
            {
                return NotFound();
            }

            return Ok(existingCity);
        }
    }
}
