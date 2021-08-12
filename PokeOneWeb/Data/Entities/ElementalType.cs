using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("ElementalType")]
    public class ElementalType : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ElementalType>().HasIndexedHashes();
            builder.Entity<ElementalType>().HasIndex(et => et.Name).IsUnique();

            builder.Entity<ElementalType>()
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

        public string PokeApiName { get; set; }

        public List<ElementalTypeRelation> AttackingDamageRelations { get; set; } = new List<ElementalTypeRelation>();
        public List<ElementalTypeRelation> DefendingDamageRelations { get; set; } = new List<ElementalTypeRelation>();
        public List<Move> Moves { get; set; } = new List<Move>();


        public override string ToString()
        {
            return Name;
        }
    }
}
