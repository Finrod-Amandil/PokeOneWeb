using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities
{
    public class AvailabilityMapper : SpreadsheetEntityMapper<AvailabilitySheetDto, PokemonAvailability>
    {
        public AvailabilityMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.PokemonAvailability;

        protected override bool IsValid(AvailabilitySheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Description);
        }

        protected override string GetUniqueName(AvailabilitySheetDto dto)
        {
            return dto.Name;
        }

        protected override PokemonAvailability MapEntity(
            AvailabilitySheetDto dto, 
            RowHash rowHash,
            PokemonAvailability availability = null)
        {
            availability ??= new PokemonAvailability();

            availability.IdHash = rowHash.IdHash;
            availability.Hash = rowHash.ContentHash;
            availability.ImportSheetId = rowHash.ImportSheetId;
            availability.Name = dto.Name;
            availability.Description = dto.Description;

            return availability;
        }
    }
}
