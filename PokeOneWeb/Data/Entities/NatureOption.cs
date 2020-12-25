using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("NatureOption")]
    public class NatureOption
    {
        public int Id { get; set; }

        [ForeignKey("NatureId")]
        public Nature Nature { get; set; }
        public int NatureId { get; set; }

        [ForeignKey("BuildId")]
        public Build Build { get; set; }
        public int BuildId { get; set; }
    }
}
