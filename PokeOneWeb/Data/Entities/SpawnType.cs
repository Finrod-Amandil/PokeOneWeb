using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("SpawnType")]
    public class SpawnType : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<SpawnType>().HasIndexedHashes();
            builder.Entity<SpawnType>().HasIndex(st => st.Name).IsUnique();
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

        public int SortIndex { get; set; }

        public bool IsSyncable { get; set; }

        public bool IsInfinite { get; set; }

        public string Color { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
