using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Spawns
{
    public class SpawnMapper : SpreadsheetEntityMapper<SpawnSheetDto, Spawn>
    {
        private readonly Dictionary<string, Season> _seasons = new();
        private readonly Dictionary<string, TimeOfDay> _timesOfDay = new();
        private readonly Dictionary<string, SpawnType> _spawnTypes = new();
        private readonly Dictionary<string, Location> _locations = new();
        private readonly Dictionary<string, PokemonForm> _pokemonForms = new();

        public SpawnMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Spawn;

        protected override bool IsValid(SpawnSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                !string.IsNullOrWhiteSpace(dto.PokemonForm) &&
                !string.IsNullOrWhiteSpace(dto.SpawnType);
        }

        protected override string GetUniqueName(SpawnSheetDto dto)
        {
            return dto.PokemonForm + dto.LocationName + dto.SpawnType + dto.Season + dto.TimeOfDay;
        }

        protected override Spawn MapEntity(SpawnSheetDto dto, RowHash rowHash, Spawn spawn = null)
        {
            spawn ??= new Spawn();

            spawn.IdHash = rowHash.IdHash;
            spawn.Hash = rowHash.ContentHash;
            spawn.ImportSheetId = rowHash.ImportSheetId;
            spawn.IsConfirmed = dto.IsConfirmed;
            spawn.LowestLevel = dto.LowestLvl;
            spawn.HighestLevel = dto.HighestLvl;
            spawn.Notes = dto.Notes;

            if (!string.IsNullOrWhiteSpace(dto.SpawnProbability))
            {
                var probabilityString = dto.SpawnProbability.TrimEnd('%');
                var canParse = decimal.TryParse(probabilityString, out var percent);

                if (!canParse)
                {
                    Reporter.ReportError(Entity, spawn.IdHash,
                        $"For spawn of {dto.PokemonForm} in {dto.LocationName} a probability was given that could not be parsed: " +
                        $"{dto.SpawnProbability}. Using unknown Commonality instead.");

                    spawn.SpawnCommonality = Spawn.UNKNOWN_COMMONALITY;
                }
                else
                {
                    var probability = percent / 100;
                    spawn.SpawnProbability = probability;
                }
            }
            else if (!string.IsNullOrWhiteSpace(dto.SpawnCommonality))
            {
                spawn.SpawnCommonality = dto.SpawnCommonality;
            }
            else if (dto.EncounterCount != null)
            {
                spawn.EncounterCount = dto.EncounterCount;
            }
            else
            {
                spawn.SpawnCommonality = Spawn.UNKNOWN_COMMONALITY;
            }

            spawn.SpawnOpportunities = MapSpawnOpportunities(dto);
            spawn.SpawnType = MapSpawnType(dto);
            spawn.PokemonForm = MapPokemonForm(dto);
            spawn.Location = MapLocation(dto);

            return spawn;
        }

        private List<SpawnOpportunity> MapSpawnOpportunities(
            SpawnSheetDto dto)
        {
            var seasons = MapSeasons(dto);
            var timesOfDay = MapTimesOfDay(dto);

            return (
                from season in seasons
                from timeOfDay in timesOfDay
                select MapSpawnOpportunity(season, timeOfDay))
            .ToList();
        }

        private SpawnOpportunity MapSpawnOpportunity(Season season, TimeOfDay timeOfDay)
        {
            var spawnOpportunity = new SpawnOpportunity
            {
                Season = season,
                TimeOfDay = timeOfDay
            };

            return spawnOpportunity;
        }

        private List<Season> MapSeasons(SpawnSheetDto dto)
        {
            var seasons = new List<Season>();

            if (string.IsNullOrWhiteSpace(dto.Season))
            {
                dto.Season = Season.ANY;
            }

            if (dto.Season.Equals(Season.ANY, StringComparison.OrdinalIgnoreCase))
            {
                if (_seasons.ContainsKey(Season.ANY))
                {
                    seasons.Add(_seasons[Season.ANY]);
                }
                else
                {
                    var season = new Season { Abbreviation = Season.ANY };
                    seasons.Add(season);
                    _seasons.Add(Season.ANY, season);
                }

                return seasons;
            }

            foreach (var abbreviation in dto.Season.ToCharArray())
            {
                if (_seasons.ContainsKey(string.Empty + abbreviation))
                {
                    seasons.Add(_seasons[string.Empty + abbreviation]);
                }
                else
                {
                    var season = new Season { Abbreviation = string.Empty + abbreviation };
                    _seasons.Add(season.Abbreviation, season);
                    seasons.Add(season);
                }
            }

            return seasons;
        }

        private List<TimeOfDay> MapTimesOfDay(SpawnSheetDto dto)
        {
            var timesOfDay = new List<TimeOfDay>();

            if (string.IsNullOrWhiteSpace(dto.TimeOfDay))
            {
                dto.TimeOfDay = TimeOfDay.ANY;
            }

            if (dto.TimeOfDay.Equals(TimeOfDay.ANY, StringComparison.OrdinalIgnoreCase))
            {
                if (_timesOfDay.ContainsKey(TimeOfDay.ANY))
                {
                    timesOfDay.Add(_timesOfDay[TimeOfDay.ANY]);
                }
                else
                {
                    var timeOfDay = new TimeOfDay { Abbreviation = TimeOfDay.ANY };
                    timesOfDay.Add(timeOfDay);
                    _timesOfDay.Add(TimeOfDay.ANY, timeOfDay);
                }

                return timesOfDay;
            }

            foreach (var abbreviation in dto.TimeOfDay.ToCharArray())
            {
                if (_timesOfDay.ContainsKey(string.Empty + abbreviation))
                {
                    timesOfDay.Add(_timesOfDay[string.Empty + abbreviation]);
                }
                else
                {
                    var timeOfDay = new TimeOfDay { Abbreviation = string.Empty + abbreviation };
                    _timesOfDay.Add(timeOfDay.Abbreviation, timeOfDay);
                    timesOfDay.Add(timeOfDay);
                }
            }

            return timesOfDay;
        }

        private SpawnType MapSpawnType(SpawnSheetDto dto)
        {
            SpawnType spawnType;
            if (!_spawnTypes.ContainsKey(dto.SpawnType))
            {
                spawnType = new SpawnType { Name = dto.SpawnType };
                _spawnTypes.Add(dto.SpawnType, spawnType);
            }
            else
            {
                spawnType = _spawnTypes[dto.SpawnType];
            }

            return spawnType;
        }

        private Location MapLocation(SpawnSheetDto dto)
        {
            Location location;
            if (!_locations.ContainsKey(dto.LocationName))
            {
                location = new Location { Name = dto.LocationName };
                _locations.Add(dto.LocationName, location);
            }
            else
            {
                location = _locations[dto.LocationName];
            }

            return location;
        }

        private PokemonForm MapPokemonForm(SpawnSheetDto dto)
        {
            PokemonForm pokemonForm;
            if (!_pokemonForms.ContainsKey(dto.PokemonForm))
            {
                pokemonForm = new PokemonForm { Name = dto.PokemonForm };
                _pokemonForms.Add(dto.PokemonForm, pokemonForm);
            }
            else
            {
                pokemonForm = _pokemonForms[dto.PokemonForm];
            }

            return pokemonForm;
        }
    }
}