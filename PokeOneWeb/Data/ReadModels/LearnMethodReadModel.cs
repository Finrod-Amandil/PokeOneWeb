﻿using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("LearnMethodReadModel")]
    public class LearnMethodReadModel : IReadModel
    {
        public int Id { get; set; }

        public bool IsAvailable { get; set; }
        public string LearnMethodName { get; set; }
        public string Description { get; set; }
        public int SortIndex { get; set; }
    }
}
