﻿using System;
namespace CityInfo.API.Models
{
    public class PointsOfInterestDto
    {
        public PointsOfInterestDto()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}