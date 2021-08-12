using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.PokeApi.Impl
{
    public class MappedPokeApiData
    {
        public IDictionary<string, PokemonSpecies> PokemonSpecies { get; set; } = new Dictionary<string, PokemonSpecies>();

        public IDictionary<string, ElementalType> ElementalTypes { get; set; } = new Dictionary<string, ElementalType>();

        public IDictionary<string, Ability> Abilities { get; set; } = new Dictionary<string, Ability>();

        public IDictionary<string, Move> Moves { get; set; } = new Dictionary<string, Move>();

        public IDictionary<string, Item> Items { get; set; } = new Dictionary<string, Item>();

        //public IEnumerable<LearnableMoveApi> LearnableMoveApis { get; set; } = new List<LearnableMoveApi>();
    }
}
