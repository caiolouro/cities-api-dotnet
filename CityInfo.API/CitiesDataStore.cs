using System;
using System.Collections.Generic;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Rio de Janeiro",
                    Description = "The city blessed by God and cursed by humans."
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "São Paulo",
                    Description = "The smoky rich city."
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Penedo",
                    Description = "The relaxing mountainous city."
                }
            };
        }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }
    }
}
