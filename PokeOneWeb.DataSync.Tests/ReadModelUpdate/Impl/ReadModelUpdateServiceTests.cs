using System.Collections.Generic;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using PokeOneWeb.DataSync.ReadModelUpdate;
using PokeOneWeb.DataSync.ReadModelUpdate.Impl;
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
        private readonly Mock<IReadModelRepository<EntityTypeReadModel>> _entityTypeRepositoryMock;
        private readonly Mock<IReadModelRepository<ItemStatBoostPokemonReadModel>> _itemStatBoostPokemonRepositoryMock;
        private readonly Mock<IReadModelRepository<SimpleLearnableMoveReadModel>> _simpleLearnableMoveRepositoryMock;
        private readonly Mock<IReadModelRepository<MoveReadModel>> _moveRepositoryMock;
        private readonly Mock<IReadModelRepository<NatureReadModel>> _natureRepositoryMock;
        private readonly Mock<IReadModelRepository<PokemonVarietyReadModel>> _pokemonVarietyRepositoryMock;
        private readonly Mock<IReadModelRepository<ItemReadModel>> _itemRepositoryMock;
        private readonly Mock<IReadModelRepository<RegionReadModel>> _regionRepositoryMock;
        private readonly Mock<IReadModelRepository<LocationGroupReadModel>> _locationGroupRepositoryMock;
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;

        #endregion Mocks

        private readonly ReadModelUpdateService _readModelUpdateService;

        public ReadModelUpdateServiceTests()
        {
            _entityTypeMapperMock = new Mock<IReadModelMapper<EntityTypeReadModel>>();
            _entityTypeMapperMock.SetReturnsDefault((IDictionary<EntityTypeReadModel, DbAction>) new Dictionary<EntityTypeReadModel, DbAction>());

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
            _locationGroupMapperMock.SetReturnsDefault((IDictionary<RegionReadModel, DbAction>)new Dictionary<RegionReadModel, DbAction>());

            _entityTypeRepositoryMock = new Mock<IReadModelRepository<EntityTypeReadModel>>();
            _itemStatBoostPokemonRepositoryMock = new Mock<IReadModelRepository<ItemStatBoostPokemonReadModel>>();
            _simpleLearnableMoveRepositoryMock = new Mock<IReadModelRepository<SimpleLearnableMoveReadModel>>();
            _moveRepositoryMock = new Mock<IReadModelRepository<MoveReadModel>>();
            _natureRepositoryMock = new Mock<IReadModelRepository<NatureReadModel>>();
            _pokemonVarietyRepositoryMock = new Mock<IReadModelRepository<PokemonVarietyReadModel>>();
            _itemRepositoryMock = new Mock<IReadModelRepository<ItemReadModel>>();
            _regionRepositoryMock = new Mock<IReadModelRepository<RegionReadModel>>();
            _locationGroupRepositoryMock = new Mock<IReadModelRepository<LocationGroupReadModel>>();
            _reporterMock = new Mock<ISpreadsheetImportReporter>();

            _readModelUpdateService = new ReadModelUpdateService(_entityTypeMapperMock.Object,
                _itemStatBoostPokemonMapperMock.Object,
                _simpleLearnableMoveMapperMock.Object,
                _moveMapperMock.Object,
                _natureMapperMock.Object,
                _pokemonVarietyMapperMock.Object,
                _itemMapperMock.Object,
                _regionMapperMock.Object,
                _locationGroupMapperMock.Object,
                _entityTypeRepositoryMock.Object,
                _itemStatBoostPokemonRepositoryMock.Object,
                _simpleLearnableMoveRepositoryMock.Object,
                _moveRepositoryMock.Object,
                _natureRepositoryMock.Object,
                _pokemonVarietyRepositoryMock.Object,
                _itemRepositoryMock.Object,
                _regionRepositoryMock.Object,
                _locationGroupRepositoryMock.Object,
                _reporterMock.Object);
        }

        [Fact]
        public void UpdateReadModel_StandardInput_SucessfullUpdate()
        {
            // Given
            SpreadsheetImportReport spreadsheetImportReport = new SpreadsheetImportReport();

            // When
            _readModelUpdateService.UpdateReadModel(spreadsheetImportReport);

            // Then
            _entityTypeMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _itemStatBoostPokemonMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _simpleLearnableMoveMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _moveMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _natureMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _pokemonVarietyMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _itemMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _regionMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));
            _locationGroupMapperMock.Verify(m => m.MapFromDatabase(spreadsheetImportReport), Times.Exactly(2));

            _entityTypeRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<EntityTypeReadModel, DbAction>>()), Times.Once());
            _itemStatBoostPokemonRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<ItemStatBoostPokemonReadModel, DbAction>>()), Times.Once());
            _simpleLearnableMoveRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<SimpleLearnableMoveReadModel, DbAction>>()), Times.Once());
            _moveRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<MoveReadModel, DbAction>>()), Times.Once());
            _natureRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<NatureReadModel, DbAction>>()), Times.Once());
            _pokemonVarietyRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<PokemonVarietyReadModel, DbAction>>()), Times.Once());
            _itemRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<ItemReadModel, DbAction>>()), Times.Once());
            _regionRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<RegionReadModel, DbAction>>()), Times.Once());
            _locationGroupRepositoryMock.Verify(m => m.Update(It.IsAny<IDictionary<LocationGroupReadModel, DbAction>>()), Times.Once());
            
            _reporterMock.Verify(m => m.StartReadModelUpdate(It.IsAny<string>()), Times.Exactly(9));
            _reporterMock.Verify(m => m.StartReadModelUpdate(), Times.Once());
            _reporterMock.Verify(m => m.StopReadModelUpdate(It.IsAny<string>()), Times.Exactly(9));
            _reporterMock.Verify(m => m.StopReadModelUpdate(), Times.Once());
        }
    }
}