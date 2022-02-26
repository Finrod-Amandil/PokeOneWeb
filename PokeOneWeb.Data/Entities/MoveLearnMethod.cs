using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
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

        public List<MoveLearnMethodLocation> Locations { get; set; } = new();


        public override string ToString()
        {
            return Name;
        }
    }
}
