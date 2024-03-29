﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Categorization of Pokemon Varieties in terms of PVP (player vs player) viability.
    /// </summary>
    [Table("PvpTier")]
    [Sheet("pvp_tiers")]
    public class PvpTier : IHashedEntity, INamedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<PvpTier>().HasIndexedHashes();
            builder.Entity<PvpTier>().HasIndex(p => p.Name).IsUnique();

            builder.Entity<PvpTier>()
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

        // INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public int SortIndex { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}