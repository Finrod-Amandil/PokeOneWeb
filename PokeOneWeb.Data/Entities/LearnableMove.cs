using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("LearnableMove")]
    public class LearnableMove
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

        [ForeignKey("PokemonVarietyId")]
        public PokemonVariety PokemonVariety { get; set; }
        public int PokemonVarietyId { get; set; }
        public List<LearnableMoveLearnMethod> LearnMethods { get; set; } = new();


        public override string ToString()
        {
            return $"{PokemonVariety} learns {Move} ({LearnMethods.Count} methods)";
        }
    }
}
