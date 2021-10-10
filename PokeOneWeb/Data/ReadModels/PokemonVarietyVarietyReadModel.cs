using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PokemonVarietyVarietyReadModel")]
    public class PokemonVarietyVarietyReadModel : IReadModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortIndex { get; set; }
        public string ResourceName { get; set; }
        public string SpriteName { get; set; }
        public string Availability { get; set; }
        public string PrimaryType { get; set; }
        public string SecondaryType { get; set; }
    }
}
