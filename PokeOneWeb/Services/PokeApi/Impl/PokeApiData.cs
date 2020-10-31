using PokeAPI;
using System.Collections.Generic;

namespace PokeOneWeb.Services.PokeApi.Impl
{
    public class PokeApiData
    {
        public IDictionary<string, PokemonSpecies> PokemonSpecies { get; set; } = new Dictionary<string, PokemonSpecies>();

        public IDictionary<string, Pokemon> PokemonVarieties { get; set; } = new Dictionary<string, Pokemon>();

        public IDictionary<string, PokemonForm> PokemonForms { get; set; } = new Dictionary<string, PokemonForm>();

        public IDictionary<string, EvolutionChain> EvolutionChains { get; set; } = new Dictionary<string, EvolutionChain>();

        public IDictionary<string, Ability> Abilities { get; set; } = new Dictionary<string, Ability>();

        public IDictionary<string, PokemonType> Types { get; set; } = new Dictionary<string, PokemonType>();

        public IDictionary<string, Move> Moves { get; set; }

        public IDictionary<string, Item> Items { get; set; }

        public IDictionary<string, MoveLearnMethod> MoveLearnMethods { get; set; }

        public IDictionary<string, VersionGroup> VersionGroups { get; set; }
    }
}
