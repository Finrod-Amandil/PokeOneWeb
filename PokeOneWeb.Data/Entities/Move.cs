﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;
using PokeOneWeb.Data.Extensions;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// A Move is an Action that a Pokemon can take during a battle. Moves can deal damage to the opponent,
    /// inflict status conditions and many other things.
    /// </summary>
    [Table("Move")]
    public class Move : IHashedEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<Move>().HasIndexedHashes();
            builder.Entity<Move>().HasIndex(m => m.ResourceName).IsUnique();
            builder.Entity<Move>().HasIndex(m => m.Name).IsUnique();

            builder.Entity<Move>()
                .HasOne(m => m.DamageClass)
                .WithMany()
                .HasForeignKey(m => m.DamageClassId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Move>()
                .HasOne(m => m.ElementalType)
                .WithMany(et => et.Moves)
                .HasForeignKey(m => m.ElementalTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Move>()
                .HasOne(x => x.ImportSheet)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        [Key]
        public int Id { get; set; }

        //INDEXED
        [Required]
        public string Hash { get; set; }

        //INDEXED
        [Required]
        public string IdHash { get; set; }

        [ForeignKey("ImportSheetId")]
        public ImportSheet ImportSheet { get; set; }
        public int ImportSheetId { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string ResourceName { get; set; }

        //INDEXED, UNIQUE
        [Required]
        public string Name { get; set; }

        public string PokeApiName { get; set; }

        public bool DoInclude { get; set; }

        /// <summary>
        /// How much damage the move inflicts. Is 0 for status moves
        /// or moves with varying damage amounts.
        /// </summary>
        public int AttackPower { get; set; }

        /// <summary>
        /// How likely the move can miss its target.
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// How often the move can be used.
        /// </summary>
        public int PowerPoints { get; set; }

        /// <summary>
        /// Moves with a priority > 0 will be used first, independent of the
        /// Speed stat of the Pokemon (ranges from +1 to +5). Similarly,
        /// a negative priority makes the Pokemon move after everyone else,
        /// regardless the speed stats involved (ranges from -1 to -7).
        /// Most moves have a priority of 0, and the Speed stats determine
        /// move order.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Description of what else the move does beside deal damage.
        /// </summary>
        public string Effect { get; set; }

        [ForeignKey("DamageClassId")]
        public MoveDamageClass DamageClass { get; set; }
        public int DamageClassId { get; set; }

        [ForeignKey("ElementalTypeId")]
        public ElementalType ElementalType { get; set; }
        public int? ElementalTypeId { get; set; }


        public override string ToString()
        {
            return $"{Name} ({ResourceName})";
        }
    }
}