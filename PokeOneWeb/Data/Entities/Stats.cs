using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Stats")]
    public class Stats
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal Attack { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal Defense { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpecialAttack { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpecialDefense { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal Speed { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal HitPoints { get; set; }

        [NotMapped]
        public decimal Total => Attack + Defense + SpecialAttack + SpecialDefense + Speed + HitPoints;
    }
}
