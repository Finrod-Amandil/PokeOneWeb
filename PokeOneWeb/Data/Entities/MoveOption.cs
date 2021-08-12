using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveOption")]
    public class MoveOption
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveOption>()
                .HasOne(mo => mo.Build)
                .WithMany(b => b.MoveOptions)
                .HasForeignKey(mo => mo.BuildId);

            builder.Entity<MoveOption>()
                .HasOne(mo => mo.Move)
                .WithMany()
                .HasForeignKey(mo => mo.MoveId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        public int MoveId { get; set; }

        [ForeignKey("BuildId")]
        public Build Build { get; set; }
        public int BuildId { get; set; }

        public int Slot { get; set; }


        public override string ToString()
        {
            return $"{Move} in Slot {Slot} for {Build}";
        }
    }
}
