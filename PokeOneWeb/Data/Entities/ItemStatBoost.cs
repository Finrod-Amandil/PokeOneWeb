using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
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

        public List<ItemStatBoostPokemon> RequiredPokemon { get; set; } = new List<ItemStatBoostPokemon>();


        public override string ToString()
        {
            return $"{Item} (ItemStatBoost)";
        }
    }
}