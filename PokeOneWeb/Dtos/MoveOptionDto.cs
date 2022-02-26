namespace PokeOneWeb.WebApi.Dtos
{
    public class MoveOptionDto
    {
        public int Slot { get; set; }
        public string MoveName { get; set; }
        public string ElementalType { get; set; }
        public string DamageClass { get; set; }
        public int AttackPower { get; set; }
        public int Accuracy { get; set; }
        public int PowerPoints { get; set; }
        public int Priority { get; set; }
        public string EffectDescription { get; set; }
    }
}
