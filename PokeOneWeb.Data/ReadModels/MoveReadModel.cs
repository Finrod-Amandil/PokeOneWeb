using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class MoveNameReadModel : IReadModel
    {
        public string Name { get; set; }
        public string ResourceName { get; set; }
    }

    public class MoveReadModel : MoveNameReadModel
    {
        public string ElementalType { get; set; }
        public string DamageClass { get; set; }
        public int AttackPower { get; set; }
        public int Accuracy { get; set; }
        public int PowerPoints { get; set; }
        public int Priority { get; set; }
        public string EffectDescription { get; set; }
    }
}