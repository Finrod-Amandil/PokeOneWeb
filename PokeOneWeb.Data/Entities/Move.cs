using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Move")]
    public class Move : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Move>().HasIndexedHashes();
            builder.Entity<Move>().HasIndex(m => m.ResourceName).IsUnique();
            builder.Entity<Move>().HasIndex(m => m.Name).IsUnique();

            builder.Entity<Move>()
                .HasOne(m => m.DamageClass)
                .WithMany()
                .HasForeignKey(m => m.DamageClassId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Move>()
                .HasOne(m => m.ElementalType)
                .WithMany(et => et.Moves)
                .HasForeignKey(m => m.ElementalTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Move>()
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
        public string ResourceName { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public string PokeApiName { get; set; }

        public bool DoInclude { get; set; }

        public int AttackPower { get; set; }

        public int Accuracy { get; set; }

        public int PowerPoints { get; set; }

        public int Priority { get; set; }

        public string Effect { get; set; }

        [ForeignKey("DamageClassId")]
        public MoveDamageClass DamageClass { get; set; }
        public int DamageClassId { get; set; }

        [ForeignKey("ElementalTypeId")]
        public ElementalType ElementalType { get; set; }
        public int? ElementalTypeId { get; set; }


        public override string ToString()
        {
            return $"{Name} ({ResourceName})";
        }
    }
}
