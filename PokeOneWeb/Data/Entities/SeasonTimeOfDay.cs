using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    public class SeasonTimeOfDay
    {
        public int Id { get; set; }

        [ForeignKey("SeasonId")]
        public Season Season { get; set; }
        public int? SeasonId { get; set; }
        
        [ForeignKey("TimeOfDayId")]
        public TimeOfDay TimeOfDay { get; set; }
        public int? TimeOfDayId { get; set; }

        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}
