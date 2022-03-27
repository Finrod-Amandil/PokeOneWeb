using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A recommendation about which nature to use for a specific build.
    /// </summary>
    [Table("NatureOption")]
    public class NatureOption
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<NatureOption>()
                .HasOne(no => no.Build)
                .WithMany(b => b.NatureOptions)
                .HasForeignKey(no => no.BuildId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<NatureOption>()
                .HasOne(no => no.Nature)
                .WithMany()
                .HasForeignKey(no => no.NatureId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("NatureId")]
        public Nature Nature { get; set; }

        public int NatureId { get; set; }

        [ForeignKey("BuildId")]
        public Build Build { get; set; }

        public int BuildId { get; set; }

        public override string ToString()
        {
            return $"{Nature} for {Build}";
        }
    }
}