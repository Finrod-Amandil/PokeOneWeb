using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SimpleLearnableMoveReadModel")]
    public class SimpleLearnableMoveReadModel
    {
        public int Id { get; set; }

        public string PokemonName { get; set; }

        public string MoveName { get; set; }
    }
}
