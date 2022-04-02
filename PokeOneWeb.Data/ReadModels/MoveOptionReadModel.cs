﻿using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("MoveOptionReadModel")]
    public class MoveOptionReadModel : IReadModel
    {
        public int Id { get; set; }
        public int ApplicationDbId { get; set; }

        public int Slot { get; set; }

        public string MoveName { get; set; }

        public string ElementalType { get; set; }

        public string DamageClass { get; set; }

        public int AttackPower { get; set; }

        public int Accuracy { get; set; }

        public int PowerPoints { get; set; }

        public int Priority { get; set; }

        public string EffectDescription { get; set; }
    }
}