using System.ComponentModel.DataAnnotations;

namespace VillaWebApp.Models.Dto
{
    public class VillaCreateDTO
    {


        [Required]
        public string Name { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string Details { get; set; }
        public string Amenity { get; set; }
        [Required]
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
    }
}
