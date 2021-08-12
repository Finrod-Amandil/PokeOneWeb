using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Item")]
    public class Item : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Item>().HasIndexedHashes();
            builder.Entity<Item>().HasIndex(i => i.Name).IsUnique();
            builder.Entity<Item>().HasIndex(i => i.ResourceName).IsUnique();

            builder.Entity<Item>()
                .HasOne(i => i.BagCategory)
                .WithMany(bc => bc.Items)
                .HasForeignKey(i => i.BagCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Item>()
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
        public string ResourceName { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public string PokeApiName { get; set; }

        public int? PokeoneItemId { get; set; }

        public int SortIndex { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public bool IsAvailable { get; set; }

        public bool DoInclude { get; set; }

        public string SpriteName { get; set; }

        [ForeignKey("BagCategoryId")]
        public BagCategory BagCategory { get; set; }
        public int BagCategoryId { get; set; }

        public List<PlacedItem> PlacedItems { get; set; } = new List<PlacedItem>();


        public override string ToString()
        {
            return $"{Name} ({ResourceName})";
        }
    }
}
