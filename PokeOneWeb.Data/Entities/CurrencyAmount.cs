using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Specifies the cost of something in a specific currency.
    /// </summary>
    [Table("CurrencyAmount")]
    public class CurrencyAmount
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<CurrencyAmount>()
                .HasOne(ca => ca.Currency)
                .WithMany()
                .HasForeignKey(ca => ca.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public int CurrencyId { get; set; }

        public string CurrencyName { internal get; set; }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}