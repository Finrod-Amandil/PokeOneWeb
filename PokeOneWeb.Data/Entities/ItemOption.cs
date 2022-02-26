using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    [Table("ItemOption")]
    public class ItemOption
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

        [ForeignKey("BuildId")]
        public Build Build { get; set; }
        public int BuildId { get; set; }


        public override string ToString()
        {
            return $"{Item} for {Build}";
        }
    }
}
