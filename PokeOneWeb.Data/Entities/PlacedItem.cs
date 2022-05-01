using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A Placed Item denotes an item that can be obtained by picking it up somewhere, i.e.
    /// items that are "laying around".
    /// </summary>
    [Table("PlacedItem")]
    [Sheet("placeditems")]
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

            builder.Entity<PlacedItem>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key]
        public int Id { get; set; }

        // INDEXED
        [Required]
        public string Hash { get; set; }

        // INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }

        public int ImportSheetId { get; set; }

        /// <summary>
        /// Gets or sets auxiliary field to distinguish placed items if the same type of item
        /// can be found multiple times in the same location.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets where in the area the item is located and how it can be found.
        /// </summary>
        public string PlacementDescription { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether some items are not visible and can be gotten if the player
        /// interacts with an object that does not look like an item, i.e.
        /// a boulder, a trash bin or a lamp post.
        /// </summary>
        public bool IsHidden { get; set; }

        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets if multiple of this item are obtained at once. Is 1 for
        /// most items.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets explicit sort index that allows sorting placed items in the
        /// order in which the player typically encounters them.
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets what prerequisites the player has to fulfill in order
        /// to be able to pick up this item.
        /// </summary>
        public string Requirements { get; set; }

        public string ScreenshotName { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int ItemId { get; set; }

        /// <summary>
        /// Sets the string-based identifier for the related item. Only used when inserting / updating records.
        /// </summary>
        [NotMapped]
        public string ItemName { internal get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public int LocationId { get; set; }

        /// <summary>
        /// Sets the string-based identifier for the related location. Only used when inserting / updating records.
        /// </summary>
        [NotMapped]
        public string LocationName { internal get; set; }

        public override string ToString()
        {
            return $"{Item?.ToString() ?? ItemName} #{Index} @ " +
                   $"{Location?.ToString() ?? LocationName}";
        }
    }
}