using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A URL to the page about this variety on another website.
    /// </summary>
    [Table("PokemonVarietyUrl")]
    public class PokemonVarietyUrl
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PokemonVarietyUrl>()
                .HasOne(pvu => pvu.Variety)
                .WithMany(pv => pv.Urls)
                .HasForeignKey(pvu => pvu.VarietyId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [ForeignKey("VarietyId")]
        public PokemonVariety Variety { get; set; }

        public int VarietyId { get; set; }

        public override string ToString()
        {
            return Url;
        }
    }
}