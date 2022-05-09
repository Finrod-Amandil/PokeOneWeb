using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class PokemonFormReadModel : IReadModel
    {
        public string Name { get; set; }
        public int SortIndex { get; set; }
        public string SpriteName { get; set; }
        public string Availability { get; set; }
    }
}