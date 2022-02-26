using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors
{
    public class MoveTutorMapper : SpreadsheetEntityMapper<MoveTutorSheetDto, MoveTutor>
    {
        private readonly Dictionary<string, Location> _locations = new();

        public MoveTutorMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.MoveTutor;

        protected override bool IsValid(MoveTutorSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.LocationName);
        }

        protected override string GetUniqueName(MoveTutorSheetDto dto)
        {
            return dto.Name + dto.LocationName;
        }

        protected override MoveTutor MapEntity(
            MoveTutorSheetDto dto, RowHash rowHash, MoveTutor moveTutor = null)
        {
            moveTutor ??= new MoveTutor();

            Location location;
            if (_locations.ContainsKey(dto.LocationName))
            {
                location = _locations[dto.LocationName];
            }
            else
            {
                location = new Location { Name = dto.LocationName };
                _locations.Add(dto.LocationName, location);
            }

            moveTutor.IdHash = rowHash.IdHash;
            moveTutor.Hash = rowHash.ContentHash;
            moveTutor.ImportSheetId = rowHash.ImportSheetId;
            moveTutor.Name = dto.Name;
            moveTutor.PlacementDescription = dto.PlacementDescription;
            moveTutor.Location = location;

            return moveTutor;
        }
    }
}
