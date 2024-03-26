using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class PokemonVarietyUrlReadModel : IReadModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}