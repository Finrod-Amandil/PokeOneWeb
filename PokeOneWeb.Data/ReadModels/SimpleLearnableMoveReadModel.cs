using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SimpleLearnableMoveReadModel")]
    public class SimpleLearnableMoveReadModel : IReadModel
    {
        public int Id { get; set; }

        public int ApplicationDbId { get; set; }

        public int PokemonVarietyApplicationDbId { get; set; }

        public string MoveResourceName { get; set; }
    }
}
