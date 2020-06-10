using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        public PointOfInterestForCreationDto()
        {
        }

        // You can configure a custom error message
        [Required(ErrorMessage = "Missing the name.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
