using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SeasonReadModel")]
    public class SeasonReadModel
    {
        public int Id { get; set; }
        public int SortIndex { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
