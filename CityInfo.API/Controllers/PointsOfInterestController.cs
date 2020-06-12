using System;
using System.Linq;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetPointOfInterest")]
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

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            // No need to check if pointOfInterest is valid because the ApiController property for the controller enforces that.
            // So, if the body is not valid and it can't be serialized to a PointOfInterestForCreationDto, it will return a bad request response automatically.

            // Custom validation that the DTO can't do
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The Name and Description can't be the same.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(auxCity => auxCity.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // Bad code, just for testing
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(auxCity => auxCity.PointsOfInterest).Max(auxPointOfInterest => auxPointOfInterest.Id);

            var pointOfInterestDto = new PointsOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(pointOfInterestDto);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = pointOfInterestDto.Id }, pointOfInterestDto);
        }

        [HttpPut("{pointOfInterestId}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The Name and Description can't be the same.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(auxCity => auxCity.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var existingPointOfInterest = city.PointsOfInterest.FirstOrDefault(auxPointOfInterest => auxPointOfInterest.Id == pointOfInterestId);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            existingPointOfInterest.Name = pointOfInterest.Name;
            existingPointOfInterest.Description = pointOfInterest.Description;

            // This returns a 204 no content response. Maybe we could prefer to return Ok with the full updated object
            return NoContent();
        }

        // By using the JsonPatchDocument, we will have out of the box compatibility with JSON Patch RFC 6902
        [HttpPatch("{pointOfInterestId}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> pointOfInterestPatchDoc)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(auxCity => auxCity.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var existingPointOfInterest = city.PointsOfInterest.FirstOrDefault(auxPointOfInterest => auxPointOfInterest.Id == pointOfInterestId);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            // TODO: All this feels a little weird, because we have 3 classes very similar for the point of interest object. Shouldn't we use just one?
            var pointOfInterestPatch = new PointOfInterestForUpdateDto()
            {
                Name = existingPointOfInterest.Name,
                Description = existingPointOfInterest.Description
            };

            // ModelState as an input allows the ApplyTo to validate the JSON patch document format
            pointOfInterestPatchDoc.ApplyTo(pointOfInterestPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // But not the validation rules for PointOfInterestForUpdateDto
            if (pointOfInterestPatch.Name == pointOfInterestPatch.Description)
            {
                ModelState.AddModelError("Description", "The Name and Description can't be the same.");
            }
            if (!TryValidateModel(pointOfInterestPatch)) // Force validation by the rules defined inside the class and check for manually added model errors (like the one above)
            {
                return BadRequest(ModelState);
            }

            // TODO: To continue the weird feeling, we want to patch specific fields, but we modify all fields, even those that are unchanged.
            existingPointOfInterest.Name = pointOfInterestPatch.Name;
            existingPointOfInterest.Description = pointOfInterestPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(auxCity => auxCity.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var existingPointOfInterest = city.PointsOfInterest.FirstOrDefault(auxPointOfInterest => auxPointOfInterest.Id == pointOfInterestId);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(existingPointOfInterest);

            return NoContent();
        }
    }
}
