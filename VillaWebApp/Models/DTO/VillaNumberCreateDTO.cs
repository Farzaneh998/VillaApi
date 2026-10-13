using System.ComponentModel.DataAnnotations;

namespace VillaWebApp.Models.Dto
{
    public class VillaNumberCreateDTO
    {
        public int VillaNo { get; set; }
        public string SpetioalDetails { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}
