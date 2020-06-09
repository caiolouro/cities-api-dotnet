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
                    Description = "The city blessed by God and cursed by humans.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 1,
                            Name = "Cristo Redentor",
                            Description = "The Chirst statue blessing the city."
                        },
                        new PointsOfInterestDto()
                        {
                            Id = 2,
                            Name = "Copacabana Beach",
                            Description = "One of the most famous and crowded beaches in the world."
                        },
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "São Paulo",
                    Description = "The smoky rich city.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 3,
                            Name = "Arena Corinthians",
                            Description = "One of World Cup 2014 stadiums that also belongs to the soccer team Corinthians."
                        },
                        new PointsOfInterestDto()
                        {
                            Id = 4,
                            Name = "Avenida Paulista",
                            Description = "A very important avenue where all the main city events take place."
                        },
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Penedo",
                    Description = "The relaxing mountainous city.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 5,
                            Name = "Casa do Papai Noel",
                            Description = "Santa Claus home in Brazil."
                        }
                    }
                }
            };
        }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }
    }
}
