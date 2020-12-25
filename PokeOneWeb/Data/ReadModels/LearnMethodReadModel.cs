using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("LearnMethodReadModel")]
    public class LearnMethodReadModel
    {
        public int Id { get; set; }

        public string LearnMethodName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
