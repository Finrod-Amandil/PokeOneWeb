using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PokemonVarietyUrlReadModel")]
    public class PokemonVarietyUrlReadModel : IReadModel
    {
        public int Id { get; set; }

        public int ApplicationDbId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
