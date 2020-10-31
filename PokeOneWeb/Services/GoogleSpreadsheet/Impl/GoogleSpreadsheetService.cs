using PokeOneWeb.Data.Entities;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.Locations;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItems;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.Spawns;
using System.Threading.Tasks;
using PokeOneWeb.Services.DataUpdate;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.LearnableMoveLearnMethods;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.TutorMoves;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl
{
    public class GoogleSpreadsheetService : IGoogleSpreadsheetService
    {
        private readonly ISpreadsheetLoader _spreadsheetLoader;
        private readonly ISpreadsheetReader<LocationDto> _locationReader;
        private readonly ISpreadsheetReader<SpawnDto> _spawnReader;
        private readonly ISpreadsheetReader<PlacedItemDto> _placedItemReader;
        private readonly ISpreadsheetReader<TutorMoveDto> _tutorMoveReader;
        private readonly ISpreadsheetReader<LearnableMoveLearnMethodDto> _learnableMoveReader;
        private readonly ISpreadsheetMapper<LocationDto, Location> _locationMapper;
        private readonly ISpreadsheetMapper<SpawnDto, Spawn> _spawnMapper;
        private readonly ISpreadsheetMapper<PlacedItemDto, PlacedItem> _placedItemMapper;
        private readonly ISpreadsheetMapper<TutorMoveDto, TutorMove> _tutorMoveMapper;
        private readonly ISpreadsheetMapper<LearnableMoveLearnMethodDto, LearnableMove> _learnableMoveMapper;
        private readonly IDataUpdateService<Location> _locationDataUpdateService;
        private readonly IDataUpdateService<Spawn> _spawnDataUpdateService;
        private readonly IDataUpdateService<PlacedItem> _placedItemDataUpdateService;
        private readonly IDataUpdateService<TutorMove> _tutorMoveUpdateService;
        private readonly IDataUpdateService<LearnableMove> _learnableMoveUpdateService;

        public GoogleSpreadsheetService(
            ISpreadsheetLoader spreadsheetLoader,
            ISpreadsheetReader<LocationDto> locationReader,
            ISpreadsheetReader<SpawnDto> spawnReader,
            ISpreadsheetReader<PlacedItemDto> placedItemReader,
            ISpreadsheetReader<TutorMoveDto> tutorMoveReader,
            ISpreadsheetReader<LearnableMoveLearnMethodDto> learnableMoveReader,
            ISpreadsheetMapper<LocationDto, Location> locationMapper,
            ISpreadsheetMapper<SpawnDto, Spawn> spawnMapper,
            ISpreadsheetMapper<PlacedItemDto, PlacedItem> placedItemMapper,
            ISpreadsheetMapper<TutorMoveDto, TutorMove> tutorMoveMapper,
            ISpreadsheetMapper<LearnableMoveLearnMethodDto, LearnableMove> learnableMoveMapper,
            IDataUpdateService<Location> locationDataUpdateService,
            IDataUpdateService<Spawn> spawnDataUpdateService,
            IDataUpdateService<PlacedItem> placedItemDataUpdateService,
            IDataUpdateService<TutorMove> tutorMoveUpdateService,
            IDataUpdateService<LearnableMove> learnableMoveUpdateService)
        {
            _spreadsheetLoader = spreadsheetLoader;
            _locationReader = locationReader;
            _spawnReader = spawnReader;
            _placedItemReader = placedItemReader;
            _tutorMoveReader = tutorMoveReader;
            _learnableMoveReader = learnableMoveReader;
            _locationMapper = locationMapper;
            _spawnMapper = spawnMapper;
            _placedItemMapper = placedItemMapper;
            _tutorMoveMapper = tutorMoveMapper;
            _learnableMoveMapper = learnableMoveMapper;
            _locationDataUpdateService = locationDataUpdateService;
            _spawnDataUpdateService = spawnDataUpdateService;
            _placedItemDataUpdateService = placedItemDataUpdateService;
            _tutorMoveUpdateService = tutorMoveUpdateService;
            _learnableMoveUpdateService = learnableMoveUpdateService;
        }

        public async Task SynchronizeSpreadsheetData()
        {
            var spreadsheet = await _spreadsheetLoader.LoadSpreadsheet(Constants.SPREADSHEET_ID);

            /*var locationDtos = _locationReader.Read(spreadsheet, Constants.SHEET_PREFIX_LOCATIONS);
            var mappedLocations = _locationMapper.Map(locationDtos);
            _locationDataUpdateService.Update(mappedLocations);

            var spawnDtos = _spawnReader.Read(spreadsheet, Constants.SHEET_PREFIX_SPAWNS);
            var mappedSpawns = _spawnMapper.Map(spawnDtos);
            _spawnDataUpdateService.Update(mappedSpawns);

            var placedItemDtos = _placedItemReader.Read(spreadsheet, Constants.SHEET_PREFIX_PLACEDITEMS);
            var mappedPlacedItems = _placedItemMapper.Map(placedItemDtos);
            _placedItemDataUpdateService.Update(mappedPlacedItems);

            var tutorMoveDtos = _tutorMoveReader.Read(spreadsheet, Constants.SHEET_PREFIX_TUTOR_MOVES);
            var mappedTutorMoves = _tutorMoveMapper.Map(tutorMoveDtos);
            _tutorMoveUpdateService.Update(mappedTutorMoves);*/

            var learnableMoveDtos = _learnableMoveReader.Read(spreadsheet, Constants.SHEET_PREFIX_LEARNABLE_MOVES);
            var mappedLearnableMoves = _learnableMoveMapper.Map(learnableMoveDtos);
            _learnableMoveUpdateService.Update(mappedLearnableMoves);
        }
    }
}
