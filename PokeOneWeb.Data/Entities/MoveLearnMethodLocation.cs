using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Describes a location, respectively an NPC in a specific location, which is able
    /// to teach moves to Pokemon.
    /// </summary>
    [Table("MoveLearnMethodLocation")]
    public class MoveLearnMethodLocation : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<MoveLearnMethodLocation>().HasIndexedHashes();

            builder.Entity<MoveLearnMethodLocation>()
                .HasOne(mlml => mlml.MoveLearnMethod)
                .WithMany(mlm => mlm.Locations)
                .HasForeignKey(mlml => mlml.MoveLearnMethodId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveLearnMethodLocation>()
                .HasOne(mlml => mlml.Location)
                .WithMany()
                .HasForeignKey(mlml => mlml.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MoveLearnMethodLocation>()
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

        public string TutorType { get; set; }

        public string NpcName { get; set; }

        /// <summary>
        /// Gets or sets where in the area the NPC is located and how to find him.
        /// </summary>
        public string PlacementDescription { get; set; }

        [ForeignKey("MoveLearnMethodId")]
        public MoveLearnMethod MoveLearnMethod { get; set; }

        public int MoveLearnMethodId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public int LocationId { get; set; }

        public List<MoveLearnMethodLocationPrice> Price { get; set; } = new();

        public override string ToString()
        {
            return $"[{MoveLearnMethod}] {TutorType} \"{NpcName}\" @ {Location}";
        }
    }
}