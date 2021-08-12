using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Builds;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Builds
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

        public IEnumerable<Build> Map(IDictionary<RowHash, BuildDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _moves = new Dictionary<string, Move>();
            _items = new Dictionary<string, Item>();
            _abilities = new Dictionary<string, Ability>();
            _natures = new Dictionary<string, Nature>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Build DTO. Skipping.");
                    continue;
                }

                yield return MapBuild(dto, rowHash);
            }
        }

        public IEnumerable<Build> MapOnto(IList<Build> entities, IDictionary<RowHash, BuildDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _pokemonVarieties = new Dictionary<string, PokemonVariety>();
            _moves = new Dictionary<string, Move>();
            _items = new Dictionary<string, Item>();
            _abilities = new Dictionary<string, Ability>();
            _natures = new Dictionary<string, Nature>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Build DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Build entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapBuild(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(BuildDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.PokemonVarietyName);
        }

        private Build MapBuild(BuildDto dto, RowHash rowHash, Build build = null)
        {
            build ??= new Build();

            build.IdHash = rowHash.IdHash;
            build.Hash = rowHash.ContentHash;
            build.ImportSheetId = rowHash.ImportSheetId;
            build.Name = dto.BuildName;
            build.Description = dto.BuildDescription;
            build.PokemonVariety = MapPokemonVariety(dto);
            build.Ability = MapAbility(dto);
            build.MoveOptions = MapMoves(dto.Move1, 1);
            build.MoveOptions.AddRange(MapMoves(dto.Move2, 2));
            build.MoveOptions.AddRange(MapMoves(dto.Move3, 3));
            build.MoveOptions.AddRange(MapMoves(dto.Move4, 4));
            build.ItemOptions = MapItems(dto);
            build.NatureOptions = MapNature(dto);

            MapEvDistribution(dto, build);

            return build;
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

        private List<MoveOption> MapMoves(string moveString, int slot)
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
                    Move = move,
                    Slot = slot
                });
            }

            return moveOptions;
        }

        private List<ItemOption> MapItems(BuildDto dto)
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
                    Item = item,
                });
            }

            return itemOptions;
        }

        private List<NatureOption> MapNature(BuildDto dto)
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
                    Nature = nature,
                });
            }

            return natureOptions;
        }

        private void MapEvDistribution(BuildDto dto, Build build)
        {
            if (string.IsNullOrWhiteSpace(dto.EvDistribution))
            {
                return;
            }

            foreach (var evPart in dto.EvDistribution.ToLowerInvariant().Split(OPTION_DIVIDER))
            {
                var evValue = evPart
                    .Replace(STAT_ATK, "").Replace(STAT_SPA, "").Replace(STAT_DEF, "")
                    .Replace(STAT_SPD, "").Replace(STAT_SPE, "").Replace(STAT_HP, "");

                if (evValue.Length == evPart.Length)
                {
                    _logger.LogWarning($"EV-Distribution part could not be parsed: {evPart}");
                    return;
                }

                evValue = evValue.Trim();

                if (!int.TryParse(evValue, out var evValueInt))
                {
                    _logger.LogWarning($"EV-Distribution part could not be parsed: {evPart}");
                    return;
                }

                if (evPart.Contains(STAT_ATK))
                {
                    build.AttackEv = evValueInt;
                }
                else if (evPart.Contains(STAT_SPA))
                {
                    build.SpecialAttackEv = evValueInt;
                }
                else if (evPart.Contains(STAT_DEF))
                {
                    build.DefenseEv = evValueInt;
                }
                else if (evPart.Contains(STAT_SPD))
                {
                    build.SpecialDefenseEv = evValueInt;
                }
                else if (evPart.Contains(STAT_SPE))
                {
                    build.SpeedEv = evValueInt;
                }
                if (evPart.Contains(STAT_HP))
                {
                    build.HitPointsEv = evValueInt;
                }
            }
        }
    }
}
