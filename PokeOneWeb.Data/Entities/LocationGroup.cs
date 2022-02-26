using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("LocationGroup")]
    public class LocationGroup
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<LocationGroup>().HasIndex(lg => lg.Name).IsUnique();
            builder.Entity<LocationGroup>().HasIndex(lg => lg.ResourceName).IsUnique();

            builder.Entity<LocationGroup>()
                .HasOne(lg => lg.Region)
                .WithMany(r => r.LocationGroups)
                .HasForeignKey(lg => lg.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string ResourceName { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        [ForeignKey("RegionId")]
        public Region Region { get; set; }
        public int RegionId { get; set; }

        public List<Location> Locations { get; set; } = new();


        public override string ToString()
        {
            return Name;
        }
    }
}
