using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Exceptions;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class SpawnSheetMapper : SheetMapper<Spawn>
    {
        public SpawnSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Spawn, object>> ValueToEntityMappings => new()
        {
            { "Location", (e, v) => e.LocationName = v.ParseAsNonEmptyString() },
            { "PokemonForm", (e, v) => e.PokemonFormName = v.ParseAsNonEmptyString() },
            { "Season", (e, v) => ApplySeasons(e, v.ParseAsNonEmptyString()) },
            { "TimeOfDay", (e, v) => ApplyTimesOfDay(e, v.ParseAsNonEmptyString()) },
            { "SpawnType", (e, v) => e.PokemonFormName = v.ParseAsNonEmptyString() },
            { "SpawnCommonality", (e, v) => e.PokemonFormName = v.ParseAsOptionalString() },
            { "SpawnProbability", (e, v) => e.SpawnProbability = ParseSpawnProbability(v.ParseAsString()) },
            { "EncounterCount", (e, v) => e.EncounterCount = v.ParseAsOptionalInt() },
            { "IsConfirmed", (e, v) => e.IsConfirmed = v.ParseAsBoolean(defaultValue: true) },
            { "LowestLevel", (e, v) => e.LowestLevel = v.ParseAsInt(defaultValue: 0) },
            { "HighestLevel", (e, v) => e.LowestLevel = v.ParseAsInt(defaultValue: 0) },
            { "Notes", (e, v) => e.Notes = v.ParseAsString() },
        };

        private static void ApplySeasons(Spawn entity, string seasonString)
        {
            var seasons = new List<string>();

            if (seasonString.Equals(Season.ANY))
            {
                seasons.Add(Season.ANY);
            }
            else
            {
                seasons.AddRange(seasonString.AsEnumerable().Select(x => x.ToString()));
            }

            if (!entity.SpawnOpportunities.Any())
            {
                entity.SpawnOpportunities.Add(new SpawnOpportunity());
            }

            foreach (var spawnOpportunity in new List<SpawnOpportunity>(entity.SpawnOpportunities))
            {
                entity.SpawnOpportunities.Remove(spawnOpportunity);
                foreach (var season in seasons)
                {
                    entity.SpawnOpportunities.Add(new SpawnOpportunity
                    {
                        SeasonAbbreviation = season,
                        TimeOfDayAbbreviation = spawnOpportunity.TimeOfDayAbbreviation
                    });
                }
            }
        }

        private static void ApplyTimesOfDay(Spawn entity, string timesOfDayString)
        {
            var timesOfDay = new List<string>();

            if (timesOfDayString.Equals(TimeOfDay.ANY))
            {
                timesOfDay.Add(TimeOfDay.ANY);
            }
            else
            {
                timesOfDay.AddRange(timesOfDayString.AsEnumerable().Select(x => x.ToString()));
            }

            if (!entity.SpawnOpportunities.Any())
            {
                entity.SpawnOpportunities.Add(new SpawnOpportunity());
            }

            foreach (var spawnOpportunity in new List<SpawnOpportunity>(entity.SpawnOpportunities))
            {
                entity.SpawnOpportunities.Remove(spawnOpportunity);
                foreach (var timeOfDay in timesOfDay)
                {
                    entity.SpawnOpportunities.Add(new SpawnOpportunity
                    {
                        SeasonAbbreviation = spawnOpportunity.SeasonAbbreviation,
                        TimeOfDayAbbreviation = timeOfDay
                    });
                }
            }
        }

        private static decimal? ParseSpawnProbability(string probabilityString)
        {
            if (string.IsNullOrWhiteSpace(probabilityString))
            {
                return null;
            }

            var canParse = decimal.TryParse(probabilityString.TrimEnd('%'), out var parsed);

            if (!canParse)
            {
                throw new ParseException(probabilityString, "decimal");
            }

            return parsed / 100M;
        }
    }
}