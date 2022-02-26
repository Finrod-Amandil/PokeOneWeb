using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("AttackEffectivityReadModel")]
    public class AttackEffectivityReadModel : IReadModel
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Effectivity { get; set; }
    }
}
