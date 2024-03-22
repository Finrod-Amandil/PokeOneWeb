using System.Collections.Generic;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.ReadModelUpdate.Impl
{
    public class ReadModelUpdateServiceTests
    {
        #region Mocks

        private readonly Mock<IReadModelMapper<EntityTypeReadModel>> _entityTypeMapperMock;
        private readonly Mock<IReadModelMapper<ItemStatBoostPokemonReadModel>> _itemStatBoostPokemonMapperMock;
        private readonly Mock<IReadModelMapper<SimpleLearnableMoveReadModel>> _simpleLearnableMoveMapperMock;
        private readonly Mock<IReadModelMapper<MoveReadModel>> _moveMapperMock;
        private readonly Mock<IReadModelMapper<NatureReadModel>> _natureMapperMock;
        private readonly Mock<IReadModelMapper<PokemonVarietyReadModel>> _pokemonVarietyMapperMock;
        private readonly Mock<IReadModelMapper<ItemReadModel>> _itemMapperMock;
        private readonly Mock<IReadModelMapper<RegionReadModel>> _regionMapperMock;
        private readonly Mock<IReadModelMapper<LocationGroupReadModel>> _locationGroupMapperMock;
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;

        #endregion Mocks

        private readonly JsonReadModelUpdateService _readModelUpdateService;

        public ReadModelUpdateServiceTests()
        {
            _entityTypeMapperMock = new Mock<IReadModelMapper<EntityTypeReadModel>>();
            _entityTypeMapperMock.SetReturnsDefault((IDictionary<EntityTypeReadModel, DbAction>)new Dictionary<EntityTypeReadModel, DbAction>());

            _itemStatBoostPokemonMapperMock = new Mock<IReadModelMapper<ItemStatBoostPokemonReadModel>>();
            _itemStatBoostPokemonMapperMock.SetReturnsDefault((IDictionary<ItemStatBoostPokemonReadModel, DbAction>)new Dictionary<ItemStatBoostPokemonReadModel, DbAction>());

            _simpleLearnableMoveMapperMock = new Mock<IReadModelMapper<SimpleLearnableMoveReadModel>>();
            _simpleLearnableMoveMapperMock.SetReturnsDefault((IDictionary<SimpleLearnableMoveReadModel, DbAction>)new Dictionary<SimpleLearnableMoveReadModel, DbAction>());

            _moveMapperMock = new Mock<IReadModelMapper<MoveReadModel>>();
            _moveMapperMock.SetReturnsDefault((IDictionary<MoveReadModel, DbAction>)new Dictionary<MoveReadModel, DbAction>());

            _natureMapperMock = new Mock<IReadModelMapper<NatureReadModel>>();
            _natureMapperMock.SetReturnsDefault((IDictionary<NatureReadModel, DbAction>)new Dictionary<NatureReadModel, DbAction>());

            _pokemonVarietyMapperMock = new Mock<IReadModelMapper<PokemonVarietyReadModel>>();
            _pokemonVarietyMapperMock.SetReturnsDefault((IDictionary<PokemonVarietyReadModel, DbAction>)new Dictionary<PokemonVarietyReadModel, DbAction>());

            _itemMapperMock = new Mock<IReadModelMapper<ItemReadModel>>();
            _itemMapperMock.SetReturnsDefault((IDictionary<ItemReadModel, DbAction>)new Dictionary<ItemReadModel, DbAction>());

            _regionMapperMock = new Mock<IReadModelMapper<RegionReadModel>>();
            _regionMapperMock.SetReturnsDefault((IDictionary<RegionReadModel, DbAction>)new Dictionary<RegionReadModel, DbAction>());

            _locationGroupMapperMock = new Mock<IReadModelMapper<LocationGroupReadModel>>();
            _locationGroupMapperMock.SetReturnsDefault((IDictionary<LocationGroupReadModel, DbAction>)new Dictionary<LocationGroupReadModel, DbAction>());

            _reporterMock = new Mock<ISpreadsheetImportReporter>();

            _readModelUpdateService = new JsonReadModelUpdateService(_entityTypeMapperMock.Object,
                _itemStatBoostPokemonMapperMock.Object,
                _simpleLearnableMoveMapperMock.Object,
                _moveMapperMock.Object,
                _natureMapperMock.Object,
                _pokemonVarietyMapperMock.Object,
                _itemMapperMock.Object,
                _regionMapperMock.Object,
                _locationGroupMapperMock.Object,
                _reporterMock.Object);
        }

        [Fact]
        public void UpdateReadModel_StandardInput_SucessfullUpdate()
        {
            // Given

            // When
            _readModelUpdateService.UpdateReadModel();

            // Then
            _entityTypeMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _itemStatBoostPokemonMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _simpleLearnableMoveMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _moveMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _natureMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _pokemonVarietyMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _itemMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _regionMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));
            _locationGroupMapperMock.Verify(m => m.MapFromDatabase(), Times.Exactly(1));

            _reporterMock.Verify(m => m.StartReadModelUpdate(It.IsAny<string>()), Times.Exactly(1));
            _reporterMock.Verify(m => m.StartReadModelUpdate(), Times.Once());
            _reporterMock.Verify(m => m.StopReadModelUpdate(It.IsAny<string>()), Times.Exactly(1));
            _reporterMock.Verify(m => m.StopReadModelUpdate(), Times.Once());
        }
    }
}