using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class LearnableMoveReadModel : IReadModel
    {
        public bool IsAvailable { get; set; }
        public string MoveName { get; set; }
        public string ElementalType { get; set; }
        public string DamageClass { get; set; }
        public int AttackPower { get; set; }
        public int EffectivePower { get; set; }
        public bool HasStab { get; set; }
        public int Accuracy { get; set; }
        public int PowerPoints { get; set; }
        public int Priority { get; set; }
        public string EffectDescription { get; set; }

        public List<LearnMethodReadModel> LearnMethods { get; set; } = new();
    }
}