using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// PokéOne specific categorization of items.
    /// </summary>
    [Table("BagCategory")]
    public class BagCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }
}
