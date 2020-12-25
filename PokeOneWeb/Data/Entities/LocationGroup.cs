using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("LocationGroup")]
    public class LocationGroup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ResourceName { get; set; }

        [ForeignKey("RegionId")]
        public Region Region { get; set; }
        public int RegionId { get; set; }

        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
