using System.Linq;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
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
            IReadModelRepository<ItemReadModel> itemRepository)
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
        }

        public void UpdateReadModel()
        {
            _entityTypeRepository.Update(_entityTypeMapper.MapFromDatabase());
            _itemStatBoostPokemonRepository.Update(_itemStatBoostPokemonMapper.MapFromDatabase());
            _simpleLearnableMoveRepository.Update(_simpleLearnableMoveMapper.MapFromDatabase());
            _moveRepository.Update(_moveMapper.MapFromDatabase());
            _natureRepository.Update(_natureMapper.MapFromDatabase());
            _pokemonVarietyRepository.Update(_pokemonVarietyMapper.MapFromDatabase());
            _itemRepository.Update(_itemMapper.MapFromDatabase());
        }
    }
}
