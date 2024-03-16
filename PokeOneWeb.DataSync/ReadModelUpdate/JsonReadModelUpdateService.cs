using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl
{
    public class JsonReadModelUpdateService : IReadModelUpdateService
    {
        private readonly IReadModelMapper<EntityTypeReadModel> _entityTypeMapper;
        private readonly IReadModelMapper<ItemStatBoostPokemonReadModel> _itemStatBoostPokemonMapper;
        private readonly IReadModelMapper<SimpleLearnableMoveReadModel> _simpleLearnableMoveMapper;
        private readonly IReadModelMapper<MoveReadModel> _moveMapper;
        private readonly IReadModelMapper<NatureReadModel> _natureMapper;
        private readonly IReadModelMapper<PokemonVarietyReadModel> _pokemonVarietyMapper;
        private readonly IReadModelMapper<ItemReadModel> _itemMapper;
        private readonly IReadModelMapper<RegionReadModel> _regionMapper;
        private readonly IReadModelMapper<LocationGroupReadModel> _locationGroupMapper;
        private readonly ISpreadsheetImportReporter _reporter;

        public JsonReadModelUpdateService(
            IReadModelMapper<EntityTypeReadModel> entityTypeMapper,
            IReadModelMapper<ItemStatBoostPokemonReadModel> itemStatBoostPokemonMapper,
            IReadModelMapper<SimpleLearnableMoveReadModel> simpleLearnableMoveMapper,
            IReadModelMapper<MoveReadModel> moveMapper,
            IReadModelMapper<NatureReadModel> natureMapper,
            IReadModelMapper<PokemonVarietyReadModel> pokemonVarietyMapper,
            IReadModelMapper<ItemReadModel> itemMapper,
            IReadModelMapper<RegionReadModel> regionMapper,
            IReadModelMapper<LocationGroupReadModel> locationGroupMapper,
            ISpreadsheetImportReporter reporter)
        {
            _entityTypeMapper = entityTypeMapper;
            _itemStatBoostPokemonMapper = itemStatBoostPokemonMapper;
            _simpleLearnableMoveMapper = simpleLearnableMoveMapper;
            _moveMapper = moveMapper;
            _natureMapper = natureMapper;
            _pokemonVarietyMapper = pokemonVarietyMapper;
            _itemMapper = itemMapper;
            _regionMapper = regionMapper;
            _locationGroupMapper = locationGroupMapper;
            _reporter = reporter;
        }

        public void UpdateReadModel()
        {
            _reporter.StartReadModelUpdate();

            _reporter.StartReadModelUpdate("generate-json-files");
            GenerateJsonFiles();
            _reporter.StopReadModelUpdate("generate-json-files");

            _reporter.StopReadModelUpdate();
        }

        private void GenerateJsonFiles()
        {
            CreateDirectories();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            // Entity types
            Console.WriteLine("generating json files for entity types");
            var entityTypes = _entityTypeMapper.MapFromDatabase();
            File.WriteAllText("resources/entity-types.json", JsonSerializer.Serialize(entityTypes, serializeOptions));

            foreach (var entityType in entityTypes)
            {
                File.WriteAllText("resources/entity-types/" + entityType.ResourceName + ".json", JsonSerializer.Serialize(entityType, serializeOptions));
            }

            // itemstats
            Console.WriteLine("generating json files for itemstats");
            var itemStats = _itemStatBoostPokemonMapper.MapFromDatabase();
            File.WriteAllText("resources/itemstats.json", JsonSerializer.Serialize(itemStats, serializeOptions));

            // learnable moves
            Console.WriteLine("generating json files for learnable-moves");
            var learnableMoves = _simpleLearnableMoveMapper.MapFromDatabase();
            learnableMoves.
                GroupBy(lmove => lmove.MoveResourceName).
                ToDictionary(lmove => lmove.Key, lmove => lmove.ToList()).
                ToList().
                ForEach(entry =>
                {
                    File.WriteAllText("resources/learnable-moves/" + entry.Key + ".json", JsonSerializer.Serialize(entry.Value, serializeOptions));
                });

            // moves
            Console.WriteLine("generating json files for moves");
            var moves = _moveMapper.MapFromDatabase();
            var listMoves = moves
                .Select(v => new
                {
                    v.ResourceName,
                    v.Name
                });
            File.WriteAllText("resources/moves.json", JsonSerializer.Serialize(listMoves, serializeOptions));
            foreach (var move in moves)
            {
                File.WriteAllText("resources/moves/" + move.ResourceName + ".json", JsonSerializer.Serialize(move, serializeOptions));
            }

            // natures
            Console.WriteLine("generating json files for natures");
            var natures = _natureMapper.MapFromDatabase();
            File.WriteAllText("resources/natures.json", JsonSerializer.Serialize(natures, serializeOptions));

            // varieties
            Console.WriteLine("generating json files for varieties");
            ICollection<PokemonVarietyReadModel> varieties = _pokemonVarietyMapper.MapFromDatabase();

            var listVarieties = varieties
                .Select(v => new
                {
                    v.ResourceName,
                    v.SortIndex,
                    v.PokedexNumber,
                    v.Name,
                    v.SpriteName,
                    v.PrimaryElementalType,
                    v.SecondaryElementalType,
                    v.Attack,
                    v.SpecialAttack,
                    v.Defense,
                    v.SpecialDefense,
                    v.Speed,
                    v.HitPoints,
                    v.StatTotal,
                    v.Bulk,
                    v.PrimaryAbility,
                    v.PrimaryAbilityEffect,
                    v.SecondaryAbility,
                    v.SecondaryAbilityEffect,
                    v.HiddenAbility,
                    v.HiddenAbilityEffect,
                    v.Availability,
                    v.PvpTier,
                    v.PvpTierSortIndex,
                    v.Generation,
                    v.IsFullyEvolved,
                    v.IsMega,
                    v.Urls,
                    v.Notes
                });
            File.WriteAllText("resources/varieties.json", JsonSerializer.Serialize(listVarieties, serializeOptions));

            foreach (var variety in varieties)
            {
                File.WriteAllText("resources/varieties/" + variety.ResourceName + ".json", JsonSerializer.Serialize(variety, serializeOptions));
            }

            // items
            Console.WriteLine("generating json files for items");
            ICollection<ItemReadModel> items = _itemMapper.MapFromDatabase();
            var listItems = items
                .Select(v => new
                {
                    v.ResourceName,
                    v.SortIndex,
                    v.Name,
                    v.Description,
                    v.Effect,
                    v.IsAvailable,
                    v.SpriteName,
                    v.BagCategoryName,
                    v.BagCategorySortIndex
                });
            File.WriteAllText("resources/items.json", JsonSerializer.Serialize(listItems, serializeOptions));

            foreach (var item in items)
            {
                File.WriteAllText("resources/items/" + item.ResourceName + ".json", JsonSerializer.Serialize(item, serializeOptions));
            }

            // regions
            Console.WriteLine("generating json files for regions");
            var regions = _regionMapper.MapFromDatabase();
            File.WriteAllText("resources/regions.json", JsonSerializer.Serialize(regions, serializeOptions));

            var locationGroups = _locationGroupMapper.MapFromDatabase();
            var locationGroupsByRegions = locationGroups.GroupBy(g => g.RegionResourceName)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var (regionResourceName, locationGroupsForRegion) in locationGroupsByRegions)
            {
                File.WriteAllText("resources/regions/" + regionResourceName + ".json", JsonSerializer.Serialize(locationGroupsForRegion, serializeOptions));
            }

            // location groups
            Console.WriteLine("generating json files for location groups");

            foreach (var locationGroup in locationGroups)
            {
                File.WriteAllText("resources/location-groups/" + locationGroup.ResourceName + ".json", JsonSerializer.Serialize(locationGroup, serializeOptions));
            }
        }

        private void CreateDirectories()
        {
            string[] directories =
            {
                "resources/entity-types",
                "resources/itemstats",
                "resources/learnable-moves",
                "resources/moves",
                "resources/natures",
                "resources/varieties",
                "resources/items",
                "resources/regions",
                "resources/location-groups",
            };

            foreach (string directory in directories)
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}