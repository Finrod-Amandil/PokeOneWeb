using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("EntityTypeReadModel")]
    public class EntityTypeReadModel
    {
        public int Id { get; set; }

        public string ResourceName { get; set; }

        public EntityType EntityType { get; set; }
    }
}
