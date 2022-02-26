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
        private readonly IReadModelRepository<EntityTypeReadModel> _entityTypeRepository;
        private readonly IReadModelRepository<ItemStatBoostPokemonReadModel> _itemStatBoostPokemonRepository;
        private readonly IReadModelRepository<SimpleLearnableMoveReadModel> _simpleLearnableMoveRepository;
        private readonly IReadModelRepository<MoveReadModel> _moveRepository;
        private readonly IReadModelRepository<NatureReadModel> _natureRepository;
        private readonly IReadModelRepository<PokemonVarietyReadModel> _pokemonVarietyRepository;
        private readonly IReadModelRepository<ItemReadModel> _itemRepository;
        private readonly ISpreadsheetImportReporter _reporter;

        public ReadModelUpdateService(
            IReadModelMapper<EntityTypeReadModel> entityTypeMapper,
            IReadModelMapper<ItemStatBoostPokemonReadModel> itemStatBoostPokemonMapper,
            IReadModelMapper<SimpleLearnableMoveReadModel> simpleLearnableMoveMapper,
            IReadModelMapper<MoveReadModel> moveMapper,
            IReadModelMapper<NatureReadModel> natureMapper,
            IReadModelMapper<PokemonVarietyReadModel> pokemonVarietyMapper,
            IReadModelMapper<ItemReadModel> itemMapper,

            IReadModelRepository<EntityTypeReadModel> entityTypeRepository,
            IReadModelRepository<ItemStatBoostPokemonReadModel> itemStatBoostPokemonRepository,
            IReadModelRepository<SimpleLearnableMoveReadModel> simpleLearnableMoveRepository,
            IReadModelRepository<MoveReadModel> moveRepository,
            IReadModelRepository<NatureReadModel> natureRepository,
            IReadModelRepository<PokemonVarietyReadModel> pokemonVarietyRepository,
            IReadModelRepository<ItemReadModel> itemRepository,
            
            ISpreadsheetImportReporter reporter)
        {
            _entityTypeMapper = entityTypeMapper;
            _itemStatBoostPokemonMapper = itemStatBoostPokemonMapper;
            _simpleLearnableMoveMapper = simpleLearnableMoveMapper;
            _moveMapper = moveMapper;
            _natureMapper = natureMapper;
            _pokemonVarietyMapper = pokemonVarietyMapper;
            _itemMapper = itemMapper;
            _entityTypeRepository = entityTypeRepository;
            _itemStatBoostPokemonRepository = itemStatBoostPokemonRepository;
            _simpleLearnableMoveRepository = simpleLearnableMoveRepository;
            _moveRepository = moveRepository;
            _natureRepository = natureRepository;
            _pokemonVarietyRepository = pokemonVarietyRepository;
            _itemRepository = itemRepository;
            _reporter = reporter;
        }

        public void UpdateReadModel(SpreadsheetImportReport importReport)
        {
            _reporter.StartReadModelUpdate();

            _reporter.StartReadModelUpdate("entityTypes");
            _entityTypeRepository.Update(_entityTypeMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("entityTypes");

            _reporter.StartReadModelUpdate("itemStatBoosts");
            _itemStatBoostPokemonRepository.Update(_itemStatBoostPokemonMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("itemStatBoosts");

            _reporter.StartReadModelUpdate("simpleLearnableMoves");
            _simpleLearnableMoveRepository.Update(_simpleLearnableMoveMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("simpleLearnableMoves");

            _reporter.StartReadModelUpdate("moves");
            _moveRepository.Update(_moveMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("moves");

            _reporter.StartReadModelUpdate("natures");
            _natureRepository.Update(_natureMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("natures");

            _reporter.StartReadModelUpdate("pokemonVarieties");
            _pokemonVarietyRepository.Update(_pokemonVarietyMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("pokemonVarieties");

            _reporter.StartReadModelUpdate("items");
            _itemRepository.Update(_itemMapper.MapFromDatabase(importReport));
            _reporter.StopReadModelUpdate("items");

            _reporter.StopReadModelUpdate();
        }
    }
}
