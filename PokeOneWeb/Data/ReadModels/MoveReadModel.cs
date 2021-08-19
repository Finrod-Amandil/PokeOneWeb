using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("MoveReadModel")]
    public class MoveReadModel : IReadModel
    {
        public int Id { get; set; }

        public int ApplicationDbId { get; set; }

        public string Name { get; set; }

        public string ResourceName { get; set; }

        public string ElementalType { get; set; }

        public string DamageClass { get; set; }

        public int AttackPower { get; set; }

        public int Accuracy { get; set; }

        public int PowerPoints { get; set; }

        public int Priority { get; set; }

        public string EffectDescription { get; set; }
    }
}
