using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("TimeOfDayReadModel")]
    public class TimeOfDayReadModel : IReadModel
    {
        public int Id { get; set; }
        public int SortIndex { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Times { get; set; }
    }
}