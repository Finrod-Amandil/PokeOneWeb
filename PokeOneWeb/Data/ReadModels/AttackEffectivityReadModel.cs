using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("AttackEffectivityReadModel")]
    public class AttackEffectivityReadModel
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Effectivity { get; set; }
    }
}
