namespace PokeOneWeb.WebApi.Dtos
{
    public class BasicPokemonVarietyDto
    {
        public string ResourceName { get; set; }
        public int SortIndex { get; set; }
        public int PokedexNumber { get; set; }
        public string Name { get; set; }
        public string SpriteName { get; set; }
        public string Availability { get; set; }
    }
}
