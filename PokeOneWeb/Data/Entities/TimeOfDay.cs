using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("TimeOfDay")]
    public class TimeOfDay
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
