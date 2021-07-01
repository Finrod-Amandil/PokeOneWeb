using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("LearnableMoveReadModel")]
    public class LearnableMoveReadModel
    {
        public int Id { get; set; }

        public bool IsAvailable { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string DamageClass { get; set; }

        public int AttackPower { get; set; }

        public int Accuracy { get; set; }

        public int PowerPoints { get; set; }

        public string EffectDescription { get; set; }

        public List<LearnMethodReadModel> LearnMethods { get; set; }
    }
}
