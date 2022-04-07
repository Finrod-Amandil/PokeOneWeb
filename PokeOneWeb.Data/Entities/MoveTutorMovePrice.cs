using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// The price in one currency that a Move Tutor charges for teaching a certain move.
    /// </summary>
    [Table("MoveTutorMovePrice")]
    public class MoveTutorMovePrice
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveTutorMovePrice>()
                .HasOne(mtmp => mtmp.MoveTutorMove)
                .WithMany(mtm => mtm.Price)
                .HasForeignKey(mtmp => mtmp.MoveTutorMoveId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveTutorMovePrice>()
                .HasOne(mtmp => mtmp.CurrencyAmount)
                .WithMany()
                .HasForeignKey(mtmp => mtmp.CurrencyAmountId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("MoveTutorMoveId")]
        public MoveTutorMove MoveTutorMove { get; set; }

        public int MoveTutorMoveId { get; set; }

        [ForeignKey("CurrencyAmountId")]
        public CurrencyAmount CurrencyAmount { get; set; }

        public int CurrencyAmountId { get; set; }
    }
}