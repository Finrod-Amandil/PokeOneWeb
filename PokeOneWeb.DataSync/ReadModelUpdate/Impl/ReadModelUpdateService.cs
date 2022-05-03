﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl
{
    public class ReadModelUpdateService : IReadModelUpdateService
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
        private readonly IReadModelRepository<EntityTypeReadModel> _entityTypeRepository;
        private readonly IReadModelRepository<ItemStatBoostPokemonReadModel> _itemStatBoostPokemonRepository;
        private readonly IReadModelRepository<SimpleLearnableMoveReadModel> _simpleLearnableMoveRepository;
        private readonly IReadModelRepository<MoveReadModel> _moveRepository;
        private readonly IReadModelRepository<NatureReadModel> _natureRepository;
        private readonly IReadModelRepository<PokemonVarietyReadModel> _pokemonVarietyRepository;
        private readonly IReadModelRepository<ItemReadModel> _itemRepository;
        private readonly IReadModelRepository<RegionReadModel> _regionRepository;
        private readonly IReadModelRepository<LocationGroupReadModel> _locationGroupRepository;
        private readonly ISpreadsheetImportReporter _reporter;

        public ReadModelUpdateService(
            IReadModelMapper<EntityTypeReadModel> entityTypeMapper,
            IReadModelMapper<ItemStatBoostPokemonReadModel> itemStatBoostPokemonMapper,
            IReadModelMapper<SimpleLearnableMoveReadModel> simpleLearnableMoveMapper,
            IReadModelMapper<MoveReadModel> moveMapper,
            IReadModelMapper<NatureReadModel> natureMapper,
            IReadModelMapper<PokemonVarietyReadModel> pokemonVarietyMapper,
            IReadModelMapper<ItemReadModel> itemMapper,
            IReadModelMapper<RegionReadModel> regionMapper,
            IReadModelMapper<LocationGroupReadModel> locationGroupMapper,
            IReadModelRepository<EntityTypeReadModel> entityTypeRepository,
            IReadModelRepository<ItemStatBoostPokemonReadModel> itemStatBoostPokemonRepository,
            IReadModelRepository<SimpleLearnableMoveReadModel> simpleLearnableMoveRepository,
            IReadModelRepository<MoveReadModel> moveRepository,
            IReadModelRepository<NatureReadModel> natureRepository,
            IReadModelRepository<PokemonVarietyReadModel> pokemonVarietyRepository,
            IReadModelRepository<ItemReadModel> itemRepository,
            IReadModelRepository<RegionReadModel> regionRepository,
            IReadModelRepository<LocationGroupReadModel> locationGroupRepository,
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
            _entityTypeRepository = entityTypeRepository;
            _itemStatBoostPokemonRepository = itemStatBoostPokemonRepository;
            _simpleLearnableMoveRepository = simpleLearnableMoveRepository;
            _moveRepository = moveRepository;
            _natureRepository = natureRepository;
            _pokemonVarietyRepository = pokemonVarietyRepository;
            _itemRepository = itemRepository;
            _regionRepository = regionRepository;
            _locationGroupRepository = locationGroupRepository;
            _reporter = reporter;
        }

        public void UpdateReadModel(SpreadsheetImportReport importReport)
        {
            _reporter.StartReadModelUpdate();

            _reporter.StartReadModelUpdate("generate-json-files");
            GenerateJsonFiles(importReport);
            _reporter.StopReadModelUpdate("generate-json-files");

            _reporter.StopReadModelUpdate();
        }

        private void GenerateJsonFiles(SpreadsheetImportReport importReport)
        {
            CreateDirectories();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            //
            // Entity types
            //
            Console.WriteLine("generating json files for entity types");
            ICollection<EntityTypeReadModel> entityTypes = _entityTypeMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/entity-types.json", JsonSerializer.Serialize(entityTypes, serializeOptions));

            foreach (var entityType in entityTypes)
            {
                File.WriteAllText("resources/entity-types/" + entityType.ResourceName + ".json", JsonSerializer.Serialize(entityType, serializeOptions));
            }

            //
            // itemstats
            //
            Console.WriteLine("generating json files for itemstats");
            ICollection<ItemStatBoostPokemonReadModel> itemStats = _itemStatBoostPokemonMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/itemstats.json", JsonSerializer.Serialize(itemStats, serializeOptions));

            //
            // learnable moves
            //
            Console.WriteLine("generating json files for learnable-moves");
            ICollection<SimpleLearnableMoveReadModel> learnableMoves = _simpleLearnableMoveMapper.MapFromDatabase(importReport).Keys;
            learnableMoves.
                GroupBy(lmove => lmove.MoveResourceName).
                ToDictionary(lmove => lmove.Key, lmove => lmove.ToList()).
                ToList().
                ForEach(entry =>
                {
                    File.WriteAllText("resources/learnable-moves/" + entry.Key + ".json", JsonSerializer.Serialize(entry.Value, serializeOptions));
                });

            //
            // moves
            //
            Console.WriteLine("generating json files for moves");
            ICollection<MoveReadModel> moves = _moveMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/moves.json", JsonSerializer.Serialize(moves, serializeOptions));
            foreach (var move in moves)
            {
                File.WriteAllText("resources/moves/" + move.ResourceName + ".json", JsonSerializer.Serialize(move, serializeOptions));
            }

            //
            // natures
            //
            Console.WriteLine("generating json files for natures");
            ICollection<NatureReadModel> natures = _natureMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/natures.json", JsonSerializer.Serialize(natures, serializeOptions));

            //
            // varieties
            //
            Console.WriteLine("generating json files for varieties");
            ICollection<PokemonVarietyReadModel> varieties = _pokemonVarietyMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/varieties.json", JsonSerializer.Serialize(varieties, serializeOptions));

            foreach (var variety in varieties)
            {
                File.WriteAllText("resources/varieties/" + variety.ResourceName + ".json", JsonSerializer.Serialize(variety, serializeOptions));
            }

            //
            // items
            //
            Console.WriteLine("generating json files for items");
            ICollection<ItemReadModel> items = _itemMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/items.json", JsonSerializer.Serialize(items, serializeOptions));

            foreach (var item in items)
            {
                File.WriteAllText("resources/items/" + item.ResourceName + ".json", JsonSerializer.Serialize(item, serializeOptions));
            }

            //
            // regions
            //
            Console.WriteLine("generating json files for regions");
            ICollection<RegionReadModel> regions = _regionMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/regions.json", JsonSerializer.Serialize(regions, serializeOptions));

            ICollection<LocationGroupReadModel> locationGroups = _locationGroupMapper.MapFromDatabase(importReport).Keys;
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
            string[] directories = new string[]{
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