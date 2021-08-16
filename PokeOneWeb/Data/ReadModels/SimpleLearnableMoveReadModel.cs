using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SimpleLearnableMoveReadModel")]
    public class SimpleLearnableMoveReadModel : IReadModel
    {
        public int Id { get; set; }

        public int ApplicationDbId { get; set; }

        public string PokemonName { get; set; }

        public string MoveName { get; set; }
    }
}
