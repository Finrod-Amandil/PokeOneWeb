﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Attributes;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Elemental Types are properties of Pokemon Varieties and Moves and influence, how effective a move of a specific type is,
    /// when used against a Pokemon with a specific type.
    /// </summary>
    [Table("ElementalType")]
    [Sheet("elemental_types")]
    public class ElementalType : IHashedEntity, INamedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ElementalType>().HasIndexedHashes();
            builder.Entity<ElementalType>().HasIndex(et => et.Name).IsUnique();

            builder.Entity<ElementalType>()
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

        public List<ElementalTypeRelation> AttackingDamageRelations { get; set; } = new();
        public List<ElementalTypeRelation> DefendingDamageRelations { get; set; } = new();
        public List<Move> Moves { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}