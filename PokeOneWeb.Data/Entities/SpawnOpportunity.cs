using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A time and season when a spawn can occur. 
    /// </summary>
    [Table("SpawnOpportunity")]
    public class SpawnOpportunity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<SpawnOpportunity>()
                .HasOne(so => so.PokemonSpawn)
                .WithMany(s => s.SpawnOpportunities)
                .HasForeignKey(so => so.PokemonSpawnId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SpawnOpportunity>()
                .HasOne(so => so.Season)
                .WithMany()
                .HasForeignKey(so => so.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SpawnOpportunity>()
                .HasOne(so => so.TimeOfDay)
                .WithMany()
                .HasForeignKey(so => so.TimeOfDayId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("PokemonSpawnId")]
        public Spawn PokemonSpawn { get; set; }
        public int PokemonSpawnId { get; set; }

        [ForeignKey("SeasonId")]
        public Season Season { get; set; }
        public int SeasonId { get; set; }

        [ForeignKey("TimeOfDayId")]
        public TimeOfDay TimeOfDay { get; set; }
        public int TimeOfDayId { get; set; }


        public override string ToString()
        {
            return $"{PokemonSpawn} @ {Season} {TimeOfDay}";
        }
    }
}
