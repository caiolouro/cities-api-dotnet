using System;
using System.Collections.Generic;
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
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>()
            {
                new { id = 1, Name = "Rio de Janeiro" },
                new { id = 2, Name = "São Paulo" }
            });
        }
    }
}
