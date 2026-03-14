using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// One of potentially multiple items that are recommended to be used in a Build.
    /// </summary>
    [Table("ItemOption")]
    public class ItemOption : IEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ItemOption>()
                .HasOne(io => io.Build)
                .WithMany(b => b.ItemOptions)
                .HasForeignKey(io => io.BuildId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ItemOption>()
                .HasOne(io => io.Item)
                .WithMany()
                .HasForeignKey(io => io.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int ItemId { get; set; }

        [NotMapped]
        public string ItemName { internal get; set; }

        [ForeignKey("BuildId")]
        public Build Build { get; set; }

        public int BuildId { get; set; }

        public override string ToString()
        {
            return $"{Item?.ToString() ?? ItemName} for {Build}";
        }
    }
}