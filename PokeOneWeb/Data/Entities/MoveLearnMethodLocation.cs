using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
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
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        public string TutorType { get; set; }

        public string NpcName { get; set; }

        public string PlacementDescription { get; set; }

        [ForeignKey("MoveLearnMethodId")]
        public MoveLearnMethod MoveLearnMethod { get; set; }
        public int MoveLearnMethodId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }

        public ICollection<MoveLearnMethodLocationPrice> Price { get; set; }


        public override string ToString()
        {
            return $"[{MoveLearnMethod}] {TutorType} \"{NpcName}\" @ {Location}";
        }
    }
}
