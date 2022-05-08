using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("LearnMethodReadModel")]
    public class LearnMethodReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public bool IsAvailable { get; set; }
        public string LearnMethodName { get; set; }
        public string Description { get; set; }
        public int SortIndex { get; set; }
    }
}