namespace PokeOneWeb.Dtos
{
    public class MoveDto
    {
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
