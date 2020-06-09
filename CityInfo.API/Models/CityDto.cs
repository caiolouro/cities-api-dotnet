using System;
using System.Collections.Generic;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public CityDto()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        // The init could be inside the constructor, but this works the same way
        public ICollection<PointsOfInterestDto> PointsOfInterest { get; set; }
            = new List<PointsOfInterestDto>();
    }
}
