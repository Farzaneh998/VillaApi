using System.ComponentModel.DataAnnotations;

namespace Rent.Models.Dto
{
    public class VillaNumberUpdateDTO
    {
        public int VillaNo { get; set; }
        public string SpetioalDetails { get; set; }
        public DateTime UpdateDate { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}
