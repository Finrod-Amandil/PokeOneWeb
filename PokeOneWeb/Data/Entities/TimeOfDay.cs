using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("TimeOfDay")]
    public class TimeOfDay
    {
        public static readonly string ANY = "Any";

        public int Id { get; set; }

        public int SortIndex { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public string Color { get; set; }

        public List<SeasonTimeOfDay> SeasonTimes { get; set; } = new List<SeasonTimeOfDay>();
    }
}
