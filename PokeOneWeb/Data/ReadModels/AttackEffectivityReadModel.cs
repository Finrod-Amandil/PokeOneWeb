using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("AttackEffectivityReadModel")]
    public class AttackEffectivityReadModel
    {
        public int Id { get; set; }

        public string TypeName { get; set; }
        public int Effectivity { get; set; }
    }
}
