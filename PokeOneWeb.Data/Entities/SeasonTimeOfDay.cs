using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Depending on the in-game season, the in-game times of day start and end at different times.
    /// </summary>
    [Table("SeasonTimeOfDay")]
    [Sheet("season_times_of_day")]
    public class SeasonTimeOfDay : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<SeasonTimeOfDay>().HasIndexedHashes();

            builder.Entity<SeasonTimeOfDay>()
                .HasOne(st => st.Season)
                .WithMany()
                .HasForeignKey(st => st.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SeasonTimeOfDay>()
                .HasOne(st => st.TimeOfDay)
                .WithMany(t => t.SeasonTimes)
                .HasForeignKey(st => st.TimeOfDayId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SeasonTimeOfDay>()
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

        public int StartHour { get; set; }

        public int EndHour { get; set; }

        [ForeignKey("SeasonId")]
        public Season Season { get; set; }

        public int? SeasonId { get; set; }

        [NotMapped]
        public string SeasonName { internal get; set; }

        [ForeignKey("TimeOfDayId")]
        public TimeOfDay TimeOfDay { get; set; }

        public int? TimeOfDayId { get; set; }

        [NotMapped]
        public string TimeOfDayName { internal get; set; }

        public override string ToString()
        {
            return $"{TimeOfDay?.ToString() ?? TimeOfDayName} in " +
                   $"{Season?.ToString() ?? SeasonName}: " +
                   $"{StartHour}:00 - {EndHour}:00";
        }
    }
}