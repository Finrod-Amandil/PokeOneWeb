using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("EntityTypeReadModel")]
    public class EntityTypeReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string ResourceName { get; set; }

        public Enums.EntityType EntityType { get; set; }
    }
}