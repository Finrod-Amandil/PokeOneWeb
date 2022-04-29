﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("EvolutionReadModel")]
    public class EvolutionReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ApplicationDbId { get; set; }

        public string BaseName { get; set; }

        public string BaseResourceName { get; set; }

        public string BaseSpriteName { get; set; }

        public string BasePrimaryElementalType { get; set; }

        public string BaseSecondaryElementalType { get; set; }

        public int BaseSortIndex { get; set; }

        public int BaseStage { get; set; }

        public string EvolvedName { get; set; }

        public string EvolvedResourceName { get; set; }

        public string EvolvedSpriteName { get; set; }

        public string EvolvedPrimaryElementalType { get; set; }

        public string EvolvedSecondaryElementalType { get; set; }

        public int EvolvedSortIndex { get; set; }

        public int EvolvedStage { get; set; }

        public string EvolutionTrigger { get; set; }

        public bool IsReversible { get; set; }

        public bool IsAvailable { get; set; }
    }
}