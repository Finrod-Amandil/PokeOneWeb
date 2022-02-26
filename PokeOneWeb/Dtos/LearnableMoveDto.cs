using System.Collections.Generic;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.WebApi.Dtos
{
    public class LearnableMoveDto
    {
        public bool IsAvailable { get; set; }
        public string MoveName { get; set; }
        public string ElementalType { get; set; }
        public string DamageClass { get; set; }
        public int BaseAttackPower { get; set; }
        public int EffectiveAttackPower { get; set; }
        public bool HasStab { get; set; }
        public int Accuracy { get; set; }
        public int PowerPoints { get; set; }
        public int Priority { get; set; }
        public string EffectDescription { get; set; }

        public IEnumerable<LearnMethodDto> LearnMethods { get; set; }
    }
}
