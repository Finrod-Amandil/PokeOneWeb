﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Season")]
    public class Season
    {
        public static readonly string ANY = "Any";

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        public string Abbreviation { get; set; }
    }
}
