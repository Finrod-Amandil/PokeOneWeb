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
        private readonly IReadModelRepository<EntityTypeReadModel> _entityTypeRepository;
        private readonly IReadModelRepository<ItemStatBoostPokemonReadModel> _itemStatBoostPokemonRepository;
        private readonly IReadModelRepository<SimpleLearnableMoveReadModel> _simpleLearnableMoveRepository;
        private readonly IReadModelRepository<MoveReadModel> _moveRepository;
        private readonly IReadModelRepository<NatureReadModel> _natureRepository;
        private readonly IReadModelRepository<PokemonVarietyReadModel> _pokemonVarietyRepository;

        public ReadModelUpdateService(
            IReadModelMapper<EntityTypeReadModel> entityTypeMapper,
            IReadModelMapper<ItemStatBoostPokemonReadModel> itemStatBoostPokemonMapper,
            IReadModelMapper<SimpleLearnableMoveReadModel> simpleLearnableMoveMapper,
            IReadModelMapper<MoveReadModel> moveMapper,
            IReadModelMapper<NatureReadModel> natureMapper,
            IReadModelMapper<PokemonVarietyReadModel> pokemonVarietyMapper,

            IReadModelRepository<EntityTypeReadModel> entityTypeRepository,
            IReadModelRepository<ItemStatBoostPokemonReadModel> itemStatBoostPokemonRepository,
            IReadModelRepository<SimpleLearnableMoveReadModel> simpleLearnableMoveRepository,
            IReadModelRepository<MoveReadModel> moveRepository,
            IReadModelRepository<NatureReadModel> natureRepository,
            IReadModelRepository<PokemonVarietyReadModel> pokemonVarietyRepository)
        {
            _entityTypeMapper = entityTypeMapper;
            _itemStatBoostPokemonMapper = itemStatBoostPokemonMapper;
            _simpleLearnableMoveMapper = simpleLearnableMoveMapper;
            _moveMapper = moveMapper;
            _natureMapper = natureMapper;
            _pokemonVarietyMapper = pokemonVarietyMapper;
            _entityTypeRepository = entityTypeRepository;
            _itemStatBoostPokemonRepository = itemStatBoostPokemonRepository;
            _simpleLearnableMoveRepository = simpleLearnableMoveRepository;
            _moveRepository = moveRepository;
            _natureRepository = natureRepository;
            _pokemonVarietyRepository = pokemonVarietyRepository;
        }

        public void UpdateReadModel()
        {
            var entityTypeReadModels = _entityTypeMapper.MapFromDatabase();
            _entityTypeRepository.Update(entityTypeReadModels);

            var itemStatBoostPokemonReadModels = _itemStatBoostPokemonMapper.MapFromDatabase();
            _itemStatBoostPokemonRepository.Update(itemStatBoostPokemonReadModels);

            var simpleLearnableMoveReadModels = _simpleLearnableMoveMapper.MapFromDatabase();
            _simpleLearnableMoveRepository.Update(simpleLearnableMoveReadModels);

            var moveReadModels = _moveMapper.MapFromDatabase();
            _moveRepository.Update(moveReadModels);

            var natureReadModels = _natureMapper.MapFromDatabase();
            _natureRepository.Update(natureReadModels);

            var pokemonVarietyReadModels = _pokemonVarietyMapper.MapFromDatabase();
            _pokemonVarietyRepository.Update(pokemonVarietyReadModels);
        }
    }
}
