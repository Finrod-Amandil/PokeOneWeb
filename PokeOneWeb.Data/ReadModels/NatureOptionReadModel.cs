using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("NatureOptionReadModel")]
    public class NatureOptionReadModel : IReadModel
    {
        public int Id { get; set; }
        public int ApplicationDbId { get; set; }
        public string NatureName { get; set; }
        public string NatureEffect { get; set; }
    }
}
