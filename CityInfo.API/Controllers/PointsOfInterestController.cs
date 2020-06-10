﻿using System;
using System.Linq;
using CityInfo.API.Models;
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
    }
}
