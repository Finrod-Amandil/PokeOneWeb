using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Availabilities
{
    public class AvailabilityImporter : SpreadsheetEntityImporter<AvailabilityDto, PokemonAvailability>
    {
        private readonly ApplicationDbContext _dbContext;

        public AvailabilityImporter(
            ISpreadsheetEntityReader<AvailabilityDto> reader, 
            ISpreadsheetEntityMapper<AvailabilityDto, PokemonAvailability> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_AVAILABILITIES;
        }

        protected override void WriteToDatabase(IEnumerable<PokemonAvailability> entities)
        {
            _dbContext.PokemonAvailabilities.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
