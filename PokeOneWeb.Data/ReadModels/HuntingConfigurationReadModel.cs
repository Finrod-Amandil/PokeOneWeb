using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("HuntingConfigurationReadModel")]
    public class HuntingConfigurationReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ApplicationDbId { get; set; }

        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }
        public string Nature { get; set; }
        public string NatureEffect { get; set; }
        public string Ability { get; set; }
    }
}