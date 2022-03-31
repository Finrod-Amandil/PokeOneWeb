using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
        private readonly IReadModelRepository<EntityTypeReadModel> _entityTypeRepository;
        private readonly IReadModelRepository<ItemStatBoostPokemonReadModel> _itemStatBoostPokemonRepository;
        private readonly IReadModelRepository<SimpleLearnableMoveReadModel> _simpleLearnableMoveRepository;
        private readonly IReadModelRepository<MoveReadModel> _moveRepository;
        private readonly IReadModelRepository<NatureReadModel> _natureRepository;
        private readonly IReadModelRepository<PokemonVarietyReadModel> _pokemonVarietyRepository;
        private readonly IReadModelRepository<ItemReadModel> _itemRepository;
        private readonly IReadModelRepository<RegionReadModel> _regionRepository;
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

            IReadModelRepository<EntityTypeReadModel> entityTypeRepository,
            IReadModelRepository<ItemStatBoostPokemonReadModel> itemStatBoostPokemonRepository,
            IReadModelRepository<SimpleLearnableMoveReadModel> simpleLearnableMoveRepository,
            IReadModelRepository<MoveReadModel> moveRepository,
            IReadModelRepository<NatureReadModel> natureRepository,
            IReadModelRepository<PokemonVarietyReadModel> pokemonVarietyRepository,
            IReadModelRepository<ItemReadModel> itemRepository,
            IReadModelRepository<RegionReadModel> regionRepository,
            
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
            _entityTypeRepository = entityTypeRepository;
            _itemStatBoostPokemonRepository = itemStatBoostPokemonRepository;
            _simpleLearnableMoveRepository = simpleLearnableMoveRepository;
            _moveRepository = moveRepository;
            _natureRepository = natureRepository;
            _pokemonVarietyRepository = pokemonVarietyRepository;
            _itemRepository = itemRepository;
            _regionRepository = regionRepository;
            _reporter = reporter;
        }

        public void UpdateReadModel(SpreadsheetImportReport importReport)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            _reporter.StartReadModelUpdate();

            Directory.CreateDirectory("resources/entities");
            Directory.CreateDirectory("resources/itemstats");
            Directory.CreateDirectory("resources/learnable-moves");
            Directory.CreateDirectory("resources/moves");
            Directory.CreateDirectory("resources/natures");
            Directory.CreateDirectory("resources/varieties");
            Directory.CreateDirectory("resources/items");
            Directory.CreateDirectory("resources/regions");

            _reporter.StartReadModelUpdate("entityTypes");

            ICollection<EntityTypeReadModel> entities = _entityTypeMapper.MapFromDatabase(importReport).Keys;

            File.WriteAllText("resources/entities.json", JsonSerializer.Serialize(entities, serializeOptions));

            foreach (var entity in entities)
            {
                File.WriteAllText("resources/entities/" + entity.ResourceName + ".json", JsonSerializer.Serialize(entity, serializeOptions));
            }

            _entityTypeRepository.Update(_entityTypeMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("entityTypes");

            _reporter.StartReadModelUpdate("itemStatBoosts");
            ICollection<ItemStatBoostPokemonReadModel> itemStats = _itemStatBoostPokemonMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/itemstats.json", JsonSerializer.Serialize(itemStats, serializeOptions));
            foreach (var itemStat in itemStats)
            {
                File.WriteAllText("resources/itemstats/" + itemStat.ItemResourceName + ".json", JsonSerializer.Serialize(itemStat, serializeOptions));
            }

            _itemStatBoostPokemonRepository.Update(_itemStatBoostPokemonMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("itemStatBoosts");

            _reporter.StartReadModelUpdate("simpleLearnableMoves");
            ICollection<SimpleLearnableMoveReadModel> learnableMoves = _simpleLearnableMoveMapper.MapFromDatabase(importReport).Keys; 
            File.WriteAllText("resources/learnable-moves.json", JsonSerializer.Serialize(learnableMoves, serializeOptions));
            foreach (var learnableMove in learnableMoves)
            {
                File.WriteAllText("resources/learnable-moves/" + learnableMove.MoveResourceName + ".json", JsonSerializer.Serialize(learnableMove, serializeOptions));
            }

            _simpleLearnableMoveRepository.Update(_simpleLearnableMoveMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("simpleLearnableMoves");

            _reporter.StartReadModelUpdate("moves");
            ICollection<MoveReadModel> moves = _moveMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/moves.json", JsonSerializer.Serialize(moves, serializeOptions));
            foreach (var move in moves)
            {
                File.WriteAllText("resources/moves/" + move.ResourceName + ".json", JsonSerializer.Serialize(move, serializeOptions));
            }

            _moveRepository.Update(_moveMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("moves");

            _reporter.StartReadModelUpdate("natures");
            ICollection<NatureReadModel> natures = _natureMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/natures.json", JsonSerializer.Serialize(natures, serializeOptions));
            foreach (var nature in natures)
            {
                File.WriteAllText("resources/natures/" + nature.Name + ".json", JsonSerializer.Serialize(nature, serializeOptions));
            }

            _natureRepository.Update(_natureMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("natures");

            _reporter.StartReadModelUpdate("pokemonVarieties");
            ICollection<PokemonVarietyReadModel> varieties = _pokemonVarietyMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/varieties.json", JsonSerializer.Serialize(varieties, serializeOptions));
            foreach (var variety in varieties)
            {
                File.WriteAllText("resources/varieties/" + variety.ResourceName + ".json", JsonSerializer.Serialize(variety, serializeOptions));
            }

            _pokemonVarietyRepository.Update(_pokemonVarietyMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("pokemonVarieties");

            _reporter.StartReadModelUpdate("items");
            ICollection<ItemReadModel> items = _itemMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/items.json", JsonSerializer.Serialize(items, serializeOptions));
            foreach (var item in items)
            {
                File.WriteAllText("resources/items/" + item.ResourceName + ".json", JsonSerializer.Serialize(item, serializeOptions));
            }

            _itemRepository.Update(_itemMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("items");

            _reporter.StartReadModelUpdate("regions");
            ICollection<RegionReadModel> regions = _regionMapper.MapFromDatabase(importReport).Keys;
            File.WriteAllText("resources/regions.json", JsonSerializer.Serialize(regions, serializeOptions));
            foreach (var region in regions)
            {
                File.WriteAllText("resources/regions/" + region.ResourceName + ".json", JsonSerializer.Serialize(region, serializeOptions));
            }

            _regionRepository.Update(_regionMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("regions");

            _reporter.StopReadModelUpdate();
        }
    }
}
