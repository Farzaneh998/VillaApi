using Microsoft.EntityFrameworkCore;
using Rent.Models;

namespace Rent.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(new Villa
            {
                Id = 1,
                Name = "villa1",
                Details = "best villa",
                Sqft = 10,
                Occupancy = 20,
                Rate = 100,
                Amenity = "tets",
                CreateDate = DateTime.Now,
                ImageUrl = "https://www.golbazar.com/%D8%B3%D9%81%D8%A7%D8%B1%D8%B4-%DA%AF%D9%84-%D9%88-%D9%87%D8%AF%DB%8C%D9%87-%D8%A8%D9%87-%D8%B3%D8%B1%D8%A7%D8%B3%D8%B1-%D8%A7%DB%8C%D8%B1%D8%A7%D9%86"
            }
            , new Villa
            {
                Id = 2,
                Name = "villa2",
                Details = "best villa",
                Sqft = 20,
                Occupancy = 50,
                Rate = 1000,
                Amenity = "tets2",
                CreateDate = DateTime.Now,
                ImageUrl = "https://www.golbazar.com/%D8%B3%D9%81%D8%A7%D8%B1%D8%B4-%DA%AF%D9%84-%D9%88-%D9%87%D8%AF%DB%8C%D9%87-%D8%A8%D9%87-%D8%B3%D8%B1%D8%A7%D8%B3%D8%B1-%D8%A7%DB%8C%D8%B1%D8%A7%D9%86"
            }
            , new Villa
            {
                Id = 3,
                Name = "villa3",
                Details = "best villa5",
                Sqft = 20,
                Occupancy = 505,
                Rate = 1000,
                Amenity = "hjf",
                CreateDate = DateTime.Now,
                ImageUrl = "https://www.golbazar.com/%D8%B3%D9%81%D8%A7%D8%B1%D8%B4-%DA%AF%D9%84-%D9%88-%D9%87%D8%AF%DB%8C%D9%87-%D8%A8%D9%87-%D8%B3%D8%B1%D8%A7%D8%B3%D8%B1-%D8%A7%DB%8C%D8%B1%D8%A7%D9%86"
            }
            );

        }
    }
}
