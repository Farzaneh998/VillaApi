using System.ComponentModel.DataAnnotations;

namespace Rent.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
    }
}
