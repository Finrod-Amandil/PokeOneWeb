using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("NatureOptionReadModel")]
    public class NatureOptionReadModel
    {
        public int Id { get; set; }
        public string NatureName { get; set; }
        public string NatureEffect { get; set; }
    }
}
