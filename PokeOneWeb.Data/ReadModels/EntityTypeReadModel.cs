using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Enums;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("EntityTypeReadModel")]
    public class EntityTypeReadModel : IReadModel
    {
        public int Id { get; set; }

        public string ResourceName { get; set; }

        public EntityType EntityType { get; set; }
    }
}
