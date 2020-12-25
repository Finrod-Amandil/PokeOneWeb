using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("HuntingConfigurationReadModel")]
    public class HuntingConfigurationReadModel
    {
        public int Id { get; set; }
        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }
        public string Nature { get; set; }
        public string NatureEffect { get; set; }
        public string Ability { get; set; }
    }
}
