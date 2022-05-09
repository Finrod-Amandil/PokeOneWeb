using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class EvolutionAbilityReadModel : IReadModel

    {
        public int RelativeStageIndex { get; set; }
        public string PokemonResourceName { get; set; }
        public int PokemonSortIndex { get; set; }
        public string PokemonName { get; set; }
        public string SpriteName { get; set; }
        public string AbilityName { get; set; }
    }
}