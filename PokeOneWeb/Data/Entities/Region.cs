using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Region")]
    public class Region
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsEventRegion { get; set; }

        public List<LocationGroup> LocationGroups { get; set; } = new List<LocationGroup>();
    }
}
