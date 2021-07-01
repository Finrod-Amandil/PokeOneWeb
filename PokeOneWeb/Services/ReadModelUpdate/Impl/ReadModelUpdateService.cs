using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class ReadModelUpdateService : IReadModelUpdateService
    {
        private readonly ReadModelDbContext _readModelDbContext;
        private readonly IReadModelMapper<PokemonReadModel> _pokemonReadModelMapper;
        private readonly IReadModelMapper<MoveReadModel> _moveReadModelMapper;
        private readonly IReadModelMapper<SimpleLearnableMoveReadModel> _simpleLearnableMoveReadModelMapper;
        private readonly IReadModelMapper<EntityTypeReadModel> _entityTypeReadModelMapper;
        private readonly IReadModelMapper<ItemStatBoostReadModel> _itemStatBoostReadModelMapper;
        private readonly IReadModelMapper<NatureReadModel> _natureReadModelMapper;

        public ReadModelUpdateService(
            ReadModelDbContext readModelDbContext,
            IReadModelMapper<PokemonReadModel> pokemonReadModelMapper,
            IReadModelMapper<MoveReadModel> moveReadModelMapper,
            IReadModelMapper<SimpleLearnableMoveReadModel> simpleLearnableMoveReadModelMapper,
            IReadModelMapper<EntityTypeReadModel> entityTypeReadModelMapper,
            IReadModelMapper<ItemStatBoostReadModel> itemStatBoostReadModelMapper,
            IReadModelMapper<NatureReadModel> natureReadModelMapper)
        {
            _readModelDbContext = readModelDbContext;
            _pokemonReadModelMapper = pokemonReadModelMapper;
            _moveReadModelMapper = moveReadModelMapper;
            _simpleLearnableMoveReadModelMapper = simpleLearnableMoveReadModelMapper;
            _entityTypeReadModelMapper = entityTypeReadModelMapper;
            _itemStatBoostReadModelMapper = itemStatBoostReadModelMapper;
            _natureReadModelMapper = natureReadModelMapper;
        }

        public void UpdateReadModel()
        {
            _readModelDbContext.Database.EnsureDeleted();
            _readModelDbContext.Database.Migrate();

            var pokemonReadModels = _pokemonReadModelMapper.MapFromDatabase();
            using var pokemonTransaction = _readModelDbContext.Database.BeginTransaction();
            _readModelDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PokeOneWebReadModel.dbo.PokemonReadModel ON");
            _readModelDbContext.PokemonReadModels.AddRange(pokemonReadModels);
            _readModelDbContext.SaveChanges();
            _readModelDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PokeOneWebReadModel.dbo.PokemonReadModel OFF");
            pokemonTransaction.Commit();

            var moveReadModels = _moveReadModelMapper.MapFromDatabase();
            using var moveTransaction = _readModelDbContext.Database.BeginTransaction();
            _readModelDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PokeOneWebReadModel.dbo.MoveReadModel ON");
            _readModelDbContext.MoveReadModels.AddRange(moveReadModels);
            _readModelDbContext.SaveChanges();
            _readModelDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PokeOneWebReadModel.dbo.MoveReadModel OFF");
            moveTransaction.Commit();

            var simpleLearnableMoveReadModels = _simpleLearnableMoveReadModelMapper.MapFromDatabase();
            using var simpleLearnableMoveTransaction = _readModelDbContext.Database.BeginTransaction();
            _readModelDbContext.SimpleLearnableMoveReadModels.AddRange(simpleLearnableMoveReadModels);
            _readModelDbContext.SaveChanges();
            simpleLearnableMoveTransaction.Commit();

            var entityTypeReadModels = _entityTypeReadModelMapper.MapFromDatabase();
            using var entityTypeTransaction = _readModelDbContext.Database.BeginTransaction();
            _readModelDbContext.EntityTypeReadModels.AddRange(entityTypeReadModels);
            _readModelDbContext.SaveChanges();
            entityTypeTransaction.Commit();

            var itemStatBoostReadModels = _itemStatBoostReadModelMapper.MapFromDatabase();
            using var itemStatBoostTransaction = _readModelDbContext.Database.BeginTransaction();
            _readModelDbContext.ItemStatBoostReadModels.AddRange(itemStatBoostReadModels);
            _readModelDbContext.SaveChanges();
            itemStatBoostTransaction.Commit();

            var natureReadModels = _natureReadModelMapper.MapFromDatabase();
            using var natureTransaction = _readModelDbContext.Database.BeginTransaction();
            _readModelDbContext.NatureReadModels.AddRange(natureReadModels);
            _readModelDbContext.SaveChanges();
            natureTransaction.Commit();
        }
    }
}
