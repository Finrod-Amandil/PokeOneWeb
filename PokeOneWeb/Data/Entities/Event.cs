using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Event")]
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("EventRegionId")]
        public Region EventRegion { get; set; }
        public int? EventRegionId { get; set; }
    }
}
