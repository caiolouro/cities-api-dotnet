using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        public PointsOfInterestController()
        {
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);

            if (existingCity == null)
            {
                return NotFound();
            }

            return Ok(existingCity.PointsOfInterest);
        }

        [HttpGet("{id}")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);

            if (existingCity == null)
            {
                return NotFound();
            }

            var existingPointOfInterest = existingCity.PointsOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == id);

            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(existingPointOfInterest);
        }
    }
}
