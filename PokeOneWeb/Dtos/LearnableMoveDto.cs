using System.Collections.Generic;

namespace PokeOneWeb.Dtos
{
    public class LearnableMoveDto
    {
        public bool IsAvailable { get; set; }
        public string MoveName { get; set; }
        public string ElementalType { get; set; }
        public string DamageClass { get; set; }
        public int AttackPower { get; set; }
        public int Accuracy { get; set; }
        public int PowerPoints { get; set; }
        public int Priority { get; set; }
        public string EffectDescription { get; set; }

        public IEnumerable<LearnMethodDto> LearnMethods { get; set; }
    }
}
