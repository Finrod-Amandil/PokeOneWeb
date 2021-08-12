using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveTutorMove")]
    public class MoveTutorMove : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveTutorMove>().HasIndexedHashes();

            builder.Entity<MoveTutorMove>()
                .HasOne(mtm => mtm.Move)
                .WithMany()
                .HasForeignKey(mtm => mtm.MoveId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveTutorMove>()
                .HasOne(mtm => mtm.MoveTutor)
                .WithMany(mt => mt.Moves)
                .HasForeignKey(mtm => mtm.MoveTutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveTutorMove>()
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

        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        public int MoveId { get; set; }

        [ForeignKey("MoveTutorId")]
        public MoveTutor MoveTutor { get; set; }
        public int MoveTutorId { get; set; }

        public List<MoveTutorMovePrice> Price { get; set; } = new List<MoveTutorMovePrice>();


        public override string ToString()
        {
            return $"{MoveTutor} teaches {Move}";
        }
    }
}
