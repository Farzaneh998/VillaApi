using System.ComponentModel.DataAnnotations;

namespace VillaWebApp.Models.Dto
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpetioalDetails { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}
