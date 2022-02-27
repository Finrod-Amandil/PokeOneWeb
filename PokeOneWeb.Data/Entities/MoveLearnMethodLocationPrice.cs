using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// When using an NPC to teach a Pokemon a move, he will charge a price, often in multiple currencies,
    /// i.e. 1000 Pokedollars and 50 Gold. This entity describes the price per currency (n-n relation).
    /// </summary>
    [Table("MoveLearnMethodLocationPrice")]
    public class MoveLearnMethodLocationPrice
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveLearnMethodLocationPrice>()
                .HasOne(mlmlp => mlmlp.MoveLearnMethodLocation)
                .WithMany(mlml => mlml.Price)
                .HasForeignKey(mlmlp => mlmlp.MoveLearnMethodLocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveLearnMethodLocationPrice>()
                .HasOne(mlmlp => mlmlp.CurrencyAmount)
                .WithMany()
                .HasForeignKey(mlmlp => mlmlp.CurrencyAmountId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("MoveLearnMethodLocationId")]
        public MoveLearnMethodLocation MoveLearnMethodLocation { get; set; }
        public int MoveLearnMethodLocationId { get; set; }

        [ForeignKey("CurrencyAmountId")]
        public CurrencyAmount CurrencyAmount { get; set; }
        public int CurrencyAmountId { get; set; }
    }
}
