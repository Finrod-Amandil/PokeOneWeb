using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds
{
    public class BuildMapper : XSpreadsheetEntityMapper<BuildSheetDto, Build>
    {
        // TODO constants
        private static readonly string OPTION_DIVIDER = "/";

        private static readonly string STAT_ATK = "atk";
        private static readonly string STAT_SPA = "spa";
        private static readonly string STAT_DEF = "def";
        private static readonly string STAT_SPD = "spd";
        private static readonly string STAT_SPE = "spe";
        private static readonly string STAT_HP = "hp";

        private readonly Dictionary<string, PokemonVariety> _pokemonVarieties = new();
        private readonly Dictionary<string, Move> _moves = new();
        private readonly Dictionary<string, Item> _items = new();
        private readonly Dictionary<string, Ability> _abilities = new();
        private readonly Dictionary<string, Nature> _natures = new();

        public BuildMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Build;

        protected override bool IsValid(BuildSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.PokemonVarietyName);
        }

        protected override string GetUniqueName(BuildSheetDto dto)
        {
            return dto.BuildName;
        }

        protected override Build MapEntity(BuildSheetDto dto, RowHash rowHash, Build build = null)
        {
            build ??= new Build();

            build.IdHash = rowHash.IdHash;
            build.Hash = rowHash.Hash;
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

        private PokemonVariety MapPokemonVariety(BuildSheetDto dto)
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

        private Ability MapAbility(BuildSheetDto dto)
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

        private List<ItemOption> MapItems(BuildSheetDto dto)
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

        private List<NatureOption> MapNature(BuildSheetDto dto)
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

        private void MapEvDistribution(BuildSheetDto dto, Build build)
        {
            if (string.IsNullOrWhiteSpace(dto.EvDistribution))
            {
                return;
            }

            foreach (var evPart in dto.EvDistribution.ToLowerInvariant().Split(OPTION_DIVIDER))
            {
                var evValue = evPart
                    .Replace(STAT_ATK, string.Empty).Replace(STAT_SPA, string.Empty).Replace(STAT_DEF, string.Empty)
                    .Replace(STAT_SPD, string.Empty).Replace(STAT_SPE, string.Empty).Replace(STAT_HP, string.Empty);

                if (evValue.Length == evPart.Length)
                {
                    Reporter.ReportError(Entity, build.IdHash, $"EV-Distribution part could not be parsed: {evPart}");
                    return;
                }

                evValue = evValue.Trim();

                if (!int.TryParse(evValue, out var evValueInt))
                {
                    Reporter.ReportError(Entity, build.IdHash, $"EV-Distribution part could not be parsed: {evPart}");
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