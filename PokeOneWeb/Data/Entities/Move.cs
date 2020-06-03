﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Move")]
    public class Move
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("DamageClassId")]
        public MoveDamageClass DamageClass { get; set; }
        public int DamageClassId { get; set; }

        [ForeignKey("ElementalTypeId")]
        public ElementalType ElementalType { get; set; }
        public int ElementalTypeId { get; set; }

        public int AttackPower { get; set; }

        public int Accuracy { get; set; }

        public int PowerPoints { get; set; }
    }
}
