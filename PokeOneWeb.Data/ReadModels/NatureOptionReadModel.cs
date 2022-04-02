using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

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