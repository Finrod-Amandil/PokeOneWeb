using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("HuntingConfigurationReadModel")]
    public class HuntingConfigurationReadModel : IReadModel
    {
        public int Id { get; set; }
        public int ApplicationDbId { get; set; }
        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }
        public string Nature { get; set; }
        public string NatureEffect { get; set; }
        public string Ability { get; set; }
    }
}