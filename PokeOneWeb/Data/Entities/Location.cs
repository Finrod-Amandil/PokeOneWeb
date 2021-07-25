﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("Location")]
    public class Location : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Location>().HasIndexedHashes();
            builder.Entity<Location>().HasIndex(l => l.Name).IsUnique();

            builder.Entity<Location>()
                .HasOne(l => l.LocationGroup)
                .WithMany(lg => lg.Locations)
                .HasForeignKey(l => l.LocationGroupId)
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

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        [Required]
        public int SortIndex { get; set; }

        public bool IsDiscoverable { get; set; }

        public string Notes { get; set; }

        [ForeignKey("LocationGroupId")]
        public LocationGroup LocationGroup { get; set; }
        public int LocationGroupId { get; set; }

        public List<Spawn> PokemonSpawns { get; set; } = new List<Spawn>();

        public List<PlacedItem> PlacedItems { get; set; } = new List<PlacedItem>();


        public override string ToString()
        {
            return Name;
        }
    }
}
