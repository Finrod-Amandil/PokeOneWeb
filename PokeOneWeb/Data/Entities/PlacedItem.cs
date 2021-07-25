using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("PlacedItem")]
    public class PlacedItem : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PlacedItem>().HasIndexedHashes();

            builder.Entity<PlacedItem>()
                .HasOne(pi => pi.Item)
                .WithMany(i => i.PlacedItems)
                .HasForeignKey(pi => pi.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PlacedItem>()
                .HasOne(pi => pi.Location)
                .WithMany(l => l.PlacedItems)
                .HasForeignKey(pi => pi.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        public int Index { get; set; }

        public string PlacementDescription { get; set; }

        public string Notes { get; set; }

        public bool IsHidden { get; set; }

        public bool IsConfirmed { get; set; }

        public int Quantity { get; set; }

        public int SortIndex { get; set; }

        public string ScreenshotName { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }


        public override string ToString()
        {
            return $"{Item} #{Index} @ {Location}";
        }
    }
}
