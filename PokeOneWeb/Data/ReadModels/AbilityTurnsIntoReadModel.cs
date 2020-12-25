using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("AbilityTurnsIntoReadModel")]
    public class AbilityTurnsIntoReadModel
    {
        public int Id { get; set; }

        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }
        public string AbilityName { get; set; }
    }
}
