using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class AttackEffectivityReadModel : IReadModel
    {
        public string TypeName { get; set; }
        public decimal Effectivity { get; set; }
    }
}