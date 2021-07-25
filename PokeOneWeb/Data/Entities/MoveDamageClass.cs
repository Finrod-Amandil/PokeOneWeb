
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveDamageClass")]
    public class MoveDamageClass : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveDamageClass>().HasIndexedHashes();
            builder.Entity<MoveDamageClass>().HasIndex(d => d.Name).IsUnique();
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
