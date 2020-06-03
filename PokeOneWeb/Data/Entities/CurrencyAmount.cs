using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Specifies the cost of something in a specific currency.
    /// </summary>
    [Table("CurrencyAmount")]
    public class CurrencyAmount
    {
        public int Id { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
    }
}
