using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Builds
{
    public class BuildMapper : ISpreadsheetEntityMapper<BuildDto, Build>
    {
        private static readonly string OPTION_DIVIDER = "/";
        private static readonly string STAT_ATK = "atk";
        private static readonly string STAT_SPA = "spa";
        private static readonly string STAT_DEF = "def";
        private static readonly string STAT_SPD = "spd";
        private static readonly string STAT_SPE = "spe";
        private static readonly string STAT_HP = "hp";

        private readonly ILogger<BuildMapper> _logger;

        private IDictionary<string, PokemonVariety> _pokemonVarieties;
        private IDictionary<string, Move> _moves;
        private IDictionary<string, Item> _items;
        private IDictionary<string, Ability> _abilities;
        private IDictionary<string, Nature> _natures;

        public BuildMapper(ILogger<BuildMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Build> Map(IEnumerable<BuildDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _moves = new Dictionary<string, Move>();
            _items = new Dictionary<string, Item>();
            _abilities = new Dictionary<string, Ability>();
            _natures = new Dictionary<string, Nature>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid location DTO with blank pokemon variety name. Skipping.");
                    continue;
                }

                var build = new Build
                {
                    Name = dto.BuildName,
                    Description = dto.BuildDescription
                };

                build.PokemonVariety = MapPokemonVariety(dto);
                build.Ability = MapAbility(dto);
                build.Moves = MapMoves(dto.Move1, 1, build);
                build.Moves.AddRange(MapMoves(dto.Move2, 2, build));
                build.Moves.AddRange(MapMoves(dto.Move3, 3, build));
                build.Moves.AddRange(MapMoves(dto.Move4, 4, build));
                build.Item = MapItems(dto, build);
                build.Nature = MapNature(dto, build);
                build.EvDistribution = MapEvDistribution(dto);

                yield return build;
            }
        }

        private bool IsValid(BuildDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.PokemonVarietyName);
        }

        private PokemonVariety MapPokemonVariety(BuildDto dto)
        {
            PokemonVariety pokemonVariety;
            if (!_pokemonVarieties.ContainsKey(dto.PokemonVarietyName))
            {
                pokemonVariety = new PokemonVariety { Name = dto.PokemonVarietyName };
                _pokemonVarieties.Add(dto.PokemonVarietyName, pokemonVariety);
            }
            else
            {
                pokemonVariety = _pokemonVarieties[dto.PokemonVarietyName];
            }

            return pokemonVariety;
        }

        private Ability MapAbility(BuildDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Ability))
            {
                return null;
            }

            Ability ability;
            if (!_abilities.ContainsKey(dto.Ability))
            {
                ability = new Ability { Name = dto.Ability };
                _abilities.Add(dto.Ability, ability);
            }
            else
            {
                ability = _abilities[dto.Ability];
            }

            return ability;
        }

        private List<MoveOption> MapMoves(string moveString, int slot, Build build)
        {
            var moveOptions = new List<MoveOption>();

            if (string.IsNullOrWhiteSpace(moveString))
            {
                return moveOptions;
            }

            foreach (var moveName in moveString.Split(OPTION_DIVIDER))
            {
                if (string.IsNullOrWhiteSpace(moveName))
                {
                    continue;
                }

                var moveNameTrimmed = moveName.Trim();
                Move move;
                if (!_moves.ContainsKey(moveNameTrimmed))
                {
                    move = new Move { Name = moveNameTrimmed };
                    _moves.Add(moveNameTrimmed, move);
                }
                else
                {
                    move = _moves[moveNameTrimmed];
                }

                moveOptions.Add(new MoveOption
                {
                    //Build = build,
                    Move = move,
                    Slot = slot
                });
            }

            return moveOptions;
        }

        private List<ItemOption> MapItems(BuildDto dto, Build build)
        {
            var itemOptions = new List<ItemOption>();

            if (string.IsNullOrWhiteSpace(dto.Item))
            {
                return itemOptions;
            }

            foreach (var itemName in dto.Item.Split(OPTION_DIVIDER))
            {
                if (string.IsNullOrWhiteSpace(itemName))
                {
                    continue;
                }

                var itemNameTrimmed = itemName.Trim();
                Item item;
                if (!_items.ContainsKey(itemNameTrimmed))
                {
                    item = new Item { Name = itemNameTrimmed };
                    _items.Add(itemNameTrimmed, item);
                }
                else
                {
                    item = _items[itemNameTrimmed];
                }

                itemOptions.Add(new ItemOption
                {
                    //Build = build,
                    Item = item,
                });
            }

            return itemOptions;
        }

        private List<NatureOption> MapNature(BuildDto dto, Build build)
        {
            var natureOptions = new List<NatureOption>();

            if (string.IsNullOrWhiteSpace(dto.Nature))
            {
                return natureOptions;
            }

            foreach (var natureName in dto.Nature.Split(OPTION_DIVIDER))
            {
                if (string.IsNullOrWhiteSpace(natureName))
                {
                    continue;
                }

                var natureNameTrimmed = natureName.Trim();
                Nature nature;
                if (!_natures.ContainsKey(natureNameTrimmed))
                {
                    nature = new Nature { Name = natureNameTrimmed };
                    _natures.Add(natureNameTrimmed, nature);
                }
                else
                {
                    nature = _natures[natureNameTrimmed];
                }

                natureOptions.Add(new NatureOption
                {
                    //Build = build,
                    Nature = nature,
                });
            }

            return natureOptions;
        }

        private Stats MapEvDistribution(BuildDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EvDistribution))
            {
                return new Stats();
            }

            var evDistribution = new Stats();

            foreach (var evPart in dto.EvDistribution.ToLowerInvariant().Split(OPTION_DIVIDER))
            {
                var evValue = evPart
                    .Replace(STAT_ATK, "").Replace(STAT_SPA, "").Replace(STAT_DEF, "")
                    .Replace(STAT_SPD, "").Replace(STAT_SPE, "").Replace(STAT_HP, "");

                if (evValue.Length == evPart.Length)
                {
                    _logger.LogWarning($"EV-Distribution part could not be parsed: {evPart}");
                    return new Stats();
                }

                evValue = evValue.Trim();

                if (!int.TryParse(evValue, out var evValueInt))
                {
                    _logger.LogWarning($"EV-Distribution part could not be parsed: {evPart}");
                    return new Stats();
                }

                if (evPart.Contains(STAT_ATK))
                {
                    evDistribution.Attack = evValueInt;
                }
                else if (evPart.Contains(STAT_SPA))
                {
                    evDistribution.SpecialAttack = evValueInt;
                }
                else if (evPart.Contains(STAT_DEF))
                {
                    evDistribution.Defense = evValueInt;
                }
                else if (evPart.Contains(STAT_SPD))
                {
                    evDistribution.SpecialDefense = evValueInt;
                }
                else if (evPart.Contains(STAT_SPE))
                {
                    evDistribution.Speed = evValueInt;
                }
                if (evPart.Contains(STAT_HP))
                {
                    evDistribution.HitPoints = evValueInt;
                }
            }

            return evDistribution;
        }
    }
}
