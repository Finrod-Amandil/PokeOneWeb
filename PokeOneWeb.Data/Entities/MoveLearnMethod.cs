using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Describes, by what means a Pokemon can learn a specific move, i.e. by Level-Up, TM, Tutor NPC...
    /// </summary>
    [Table("MoveLearnMethod")]
    public class MoveLearnMethod
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveLearnMethod>().HasIndex(mlm => mlm.Name).IsUnique();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets for NPC-based methods, a list of all locations where this kind of NPC
        /// can be found.
        /// </summary>
        public List<MoveLearnMethodLocation> Locations { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}