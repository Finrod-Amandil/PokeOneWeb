using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.Spawns
{
    public class SpawnMapper : ISpreadsheetMapper<SpawnDto, Spawn>
    {
        private readonly ILogger<SpawnMapper> _logger;

        private IDictionary<string, Season> _seasons;
        private IDictionary<string, TimeOfDay> _timesOfDay;
        private IDictionary<string, SpawnType> _spawnTypes;
        private IDictionary<string, Location> _locations;
        private IDictionary<string, PokemonForm> _pokemonForms;

        public SpawnMapper(ILogger<SpawnMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Spawn> Map(IEnumerable<SpawnDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _seasons = new Dictionary<string, Season>();
            _timesOfDay = new Dictionary<string, TimeOfDay>();
            _spawnTypes = new Dictionary<string, SpawnType>();
            _locations = new Dictionary<string, Location>();
            _pokemonForms = new Dictionary<string, PokemonForm>();

            var spawns = new List<Spawn>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Spawn DTO with PokemonForm name {dto.PokemonForm} and LocationName {dto.LocationName}. Skipping.");
                    continue;
                }

                var seasons = MapSeasons(dto);
                var timesOfDay = MapTimesOfDay(dto);
                var spawnOpportunities = MapSpawnOpportunities(seasons, timesOfDay, dto);
                var spawnType = MapSpawnType(dto);
                var location = MapLocation(dto);
                var pokemonForm = MapPokemonForm(dto);

                var spawn = new Spawn
                {
                    SpawnOpportunities = spawnOpportunities.ToList(),
                    SpawnType = spawnType,
                    Location = location,
                    PokemonForm = pokemonForm,
                    IsConfirmed = dto.IsConfirmed,
                    Notes = dto.Notes
                };

                spawns.Add(spawn);
            }

            return spawns;
        }

        private bool IsValid(SpawnDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                !string.IsNullOrWhiteSpace(dto.PokemonForm) &&
                !string.IsNullOrWhiteSpace(dto.SpawnType);
        }

        private IEnumerable<Season> MapSeasons(SpawnDto dto)
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
                    var season = new Season {Abbreviation = Season.ANY};
                    seasons.Add(season);
                    _seasons.Add(Season.ANY, season);
                }

                return seasons;
            }

            foreach (var abbreviation in dto.Season.ToCharArray())
            {
                if (_seasons.ContainsKey("" + abbreviation))
                {
                    seasons.Add(_seasons["" + abbreviation]);
                }
                else
                {
                    var season = new Season {Abbreviation = "" + abbreviation};
                    _seasons.Add(season.Abbreviation, season);
                    seasons.Add(season);
                }
            }

            return seasons;
        }

        private IEnumerable<TimeOfDay> MapTimesOfDay(SpawnDto dto)
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
                if (_timesOfDay.ContainsKey("" + abbreviation))
                {
                    timesOfDay.Add(_timesOfDay["" + abbreviation]);
                }
                else
                {
                    var timeOfDay = new TimeOfDay { Abbreviation = "" + abbreviation };
                    _timesOfDay.Add(timeOfDay.Abbreviation, timeOfDay);
                    timesOfDay.Add(timeOfDay);
                }
            }

            return timesOfDay;
        }

        private IEnumerable<SpawnOpportunity> MapSpawnOpportunities(IEnumerable<Season> seasons, IEnumerable<TimeOfDay> timesOfDay, SpawnDto dto)
        {
            var spawnOpportunities = new List<SpawnOpportunity>();

            foreach (var season in seasons)
            {
                foreach (var timeOfDay in timesOfDay)
                {
                    spawnOpportunities.Add(MapSpawnOpportunity(season, timeOfDay, dto));
                }
            }

            return spawnOpportunities;
        }

        private SpawnOpportunity MapSpawnOpportunity(Season season, TimeOfDay timeOfDay, SpawnDto dto)
        {
            var spawnOpportunity = new SpawnOpportunity
            {
                Season = season,
                TimeOfDay = timeOfDay
            };

            if (!string.IsNullOrWhiteSpace(dto.SpawnProbability))
            {
                var probabilityString = dto.SpawnProbability.TrimEnd('%');
                var canParse = decimal.TryParse(probabilityString, out var percent);

                if (!canParse)
                {
                    _logger.LogWarning($"For spawn of { dto.PokemonForm } in { dto.LocationName } a probability was given that could not be parsed: " +
                                       $"{ dto.SpawnProbability }. Using unknown Commonality instead.");
                    spawnOpportunity.SpawnCommonality = SpawnOpportunity.UNKNOWN_COMMONALITY;
                }
                else
                {
                    var probability = percent / 100;
                    spawnOpportunity.SpawnProbability = probability;
                }
            }
            else if (!string.IsNullOrWhiteSpace(dto.SpawnCommonality))
            {
                spawnOpportunity.SpawnCommonality = dto.SpawnCommonality;
            }
            else if (dto.EncounterCount != null)
            {
                spawnOpportunity.EncounterCount = dto.EncounterCount;
            }
            else
            {
                spawnOpportunity.SpawnCommonality = SpawnOpportunity.UNKNOWN_COMMONALITY;
            }

            return spawnOpportunity;
        }

        private SpawnType MapSpawnType(SpawnDto dto)
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

        private Location MapLocation(SpawnDto dto)
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

        private PokemonForm MapPokemonForm(SpawnDto dto)
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
