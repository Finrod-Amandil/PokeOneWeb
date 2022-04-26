using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("LearnableMoveLearnMethod")]
    public class LearnableMoveLearnMethod : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<LearnableMoveLearnMethod>().HasIndexedHashes();

            builder.Entity<LearnableMoveLearnMethod>()
                .HasOne(lmlm => lmlm.LearnableMove)
                .WithMany(lm => lm.LearnMethods)
                .HasForeignKey(lmlm => lmlm.LearnableMoveId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LearnableMoveLearnMethod>()
                .HasOne(lmlm => lmlm.MoveLearnMethod)
                .WithMany()
                .HasForeignKey(lmlm => lmlm.MoveLearnMethodId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LearnableMoveLearnMethod>()
                .HasOne(lmlm => lmlm.RequiredItem)
                .WithMany()
                .HasForeignKey(lmlm => lmlm.RequiredItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<LearnableMoveLearnMethod>()
                .HasOne(lmlm => lmlm.MoveTutorMove)
                .WithMany()
                .HasForeignKey(lmlm => lmlm.MoveTutorMoveId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<LearnableMoveLearnMethod>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key]
        public int Id { get; set; }

        // INDEXED
        [Required]
        public string Hash { get; set; }

        // INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }

        public int ImportSheetId { get; set; }

        public bool IsAvailable { get; set; }

        public int? LevelLearnedAt { get; set; } // Only used for learn method Level Up

        public string Comments { get; set; }

        [ForeignKey("LearnableMoveId")]
        public LearnableMove LearnableMove { get; set; } = new();

        public int LearnableMoveId { get; set; }

        [ForeignKey("MoveLearnMethodId")]
        public MoveLearnMethod MoveLearnMethod { get; set; } = new();

        public int MoveLearnMethodId { get; set; }

        [ForeignKey("RequiredItemId")]
        public Item RequiredItem { get; set; }

        public int? RequiredItemId { get; set; } // Only used for learn method Machine / TM/HM

        public string RequiredItemName { internal get; set; }

        [ForeignKey("MoveTutorMoveId")]
        public MoveTutorMove MoveTutorMove { get; set; }

        public int? MoveTutorMoveId { get; set; } // Only used for learn method Tutor

        public string MoveTutorName { internal get; set; }

        public override string ToString()
        {
            return $"{LearnableMove}, method: {MoveLearnMethod}";
        }
    }
}