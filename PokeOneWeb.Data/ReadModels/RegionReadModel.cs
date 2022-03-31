﻿using PokeOneWeb.Data.ReadModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("RegionReadModel")]
    public class RegionReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required, JsonIgnore]
        public int ApplicationDbId { get; set; }

        public string ResourceName { get; set; }

        public string Name { get; set; }

        public bool IsEventRegion { get; set; }

        public string EventName { get; set; }

        public DateTime? EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; }

        public string Color { get; set; }
    }
}
