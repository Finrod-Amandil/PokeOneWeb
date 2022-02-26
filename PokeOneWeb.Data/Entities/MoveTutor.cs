using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveTutor")]
    public class MoveTutor : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveTutor>().HasIndexedHashes();
            builder.Entity<MoveTutor>().HasIndex(mt => mt.Name).IsUnique();

            builder.Entity<MoveTutor>()
                .HasOne(mt => mt.Location)
                .WithMany()
                .HasForeignKey(mt => mt.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveTutor>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key] 
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }
        public int ImportSheetId { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public string PlacementDescription { get; set; }

        [ForeignKey("LocationId")]
        public Location Location{ get; set; }
        public int LocationId { get; set; }

        public ICollection<MoveTutorMove> Moves { get; set; }


        public override string ToString()
        {
            return $"{Name}@{Location.Name}";
        }
    }
}
