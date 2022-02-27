using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// The Pokemon World is made up from Locations. A single location can be an
    /// overground route, a town/city, a cave, a building (or part of a building, i.e.
    /// floor of a tower) and so on.
    /// </summary>
    [Table("Location")]
    public class Location : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Location>().HasIndexedHashes();
            builder.Entity<Location>().HasIndex(l => l.Name).IsUnique();

            builder.Entity<Location>()
                .HasOne(l => l.LocationGroup)
                .WithMany(lg => lg.Locations)
                .HasForeignKey(l => l.LocationGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Location>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }
        public int ImportSheetId { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        [Required]
        public int SortIndex { get; set; }

        /// <summary>
        /// Locations which are discoverable contribute towards certain in-game achievements ("Discovered X locations")
        /// </summary>
        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }

        [ForeignKey("LocationGroupId")]
        public LocationGroup LocationGroup { get; set; }
        public int LocationGroupId { get; set; }

        public List<Spawn> PokemonSpawns { get; set; } = new();

        public List<PlacedItem> PlacedItems { get; set; } = new();


        public override string ToString()
        {
            return Name;
        }
    }
}
