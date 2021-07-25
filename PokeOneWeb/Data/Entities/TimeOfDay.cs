﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Data.Entities
{
    [Table("TimeOfDay")]
    public class TimeOfDay : IHashedEntity
    {
        public static readonly string ANY = "Any";

        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<TimeOfDay>().HasIndexedHashes();
            builder.Entity<TimeOfDay>().HasIndex(tod => tod.Abbreviation).IsUnique();
            builder.Entity<TimeOfDay>().HasIndex(tod => tod.Name).IsUnique();
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

        //INDEXED, UNIQUE
        [Required]
        public string Abbreviation { get; set; }

        public int SortIndex { get; set; }

        public string Color { get; set; }

        public List<SeasonTimeOfDay> SeasonTimes { get; set; } = new List<SeasonTimeOfDay>();


        public override string ToString()
        {
            return $"{Name} ({Abbreviation})";
        }
    }
}
