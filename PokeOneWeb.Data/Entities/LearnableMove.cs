using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A LearnableMove indicates, that a PokemonVariety is able to learn a certain move. A move can
    /// be learned through one or multiple methods (--> LearnableMoveLearnMethod).
    /// </summary>
    [Table("LearnableMove")]
    public class LearnableMove : IEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<LearnableMove>()
                .HasOne(lm => lm.PokemonVariety)
                .WithMany(pv => pv.LearnableMoves)
                .HasForeignKey(lm => lm.PokemonVarietyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LearnableMove>()
                .HasOne(lm => lm.Move)
                .WithMany()
                .HasForeignKey(lm => lm.MoveId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("MoveId")]
        public Move Move { get; set; }

        public int MoveId { get; set; }

        [NotMapped]
        public string MoveName { internal get; set; }

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }

        public int PokemonVarietyId { get; set; }

        [NotMapped]
        public string PokemonVarietyName { internal get; set; }

        public List<LearnableMoveLearnMethod> LearnMethods { get; set; } = new();

        public override string ToString()
        {
            return $"{PokemonVariety?.ToString() ?? PokemonVarietyName} learns " +
                   $"{Move?.ToString() ?? MoveName} ({LearnMethods.Count} methods)";
        }
    }
}