using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Additional information about certain items which can boost (or reduce)
    /// the stats of a Pokemon during battle. Certain items only boost the stats
    /// only if held by a specific Pokemon.
    /// </summary>
    [Table("ItemStatBoost")]
    public class ItemStatBoost
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ItemStatBoost>()
                .HasOne(isb => isb.Item)
                .WithMany()
                .HasForeignKey(isb => isb.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal AttackBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal DefenseBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SpecialAttackBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SpecialDefenseBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal SpeedBoost { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal HitPointsBoost { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets for which Pokemon this stat boost is available. If no Pokemon are listed,
        /// a single entry is available where the required Pokemon is null.
        /// </summary>
        public List<ItemStatBoostPokemon> RequiredPokemon { get; set; } = new();

        public override string ToString()
        {
            return $"{Item} (ItemStatBoost)";
        }
    }
}