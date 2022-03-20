using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.Extensions;
using System.Collections.Generic;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Elemental Types are properties of Pokemon Varieties and Moves and influence, how effective a move of a specific type is,
    /// when used against a Pokemon with a specific type.
    /// </summary>
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

        public List<ElementalTypeRelation> AttackingDamageRelations { get; set; } = new();
        public List<ElementalTypeRelation> DefendingDamageRelations { get; set; } = new();
        public List<Move> Moves { get; set; } = new();


        public override string ToString()
        {
            return Name;
        }
    }
}
