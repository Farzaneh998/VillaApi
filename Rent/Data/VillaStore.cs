using Rent.Models.Dto;

namespace Rent.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO> {
            new VillaDTO { Id = 1, Name = "Villa1", Sqft=100, Occupancy=50  },
           new VillaDTO {Id=2,Name="Villa2", Sqft=150 , Occupancy=250}};
    }
}
