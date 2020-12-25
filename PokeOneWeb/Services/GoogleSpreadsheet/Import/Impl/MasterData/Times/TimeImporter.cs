using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Times
{
    public class TimeImporter : SpreadsheetEntityImporter<TimeDto, SeasonTimeOfDay>
    {
        private readonly ApplicationDbContext _dbContext;

        public TimeImporter(
            ISpreadsheetEntityReader<TimeDto> reader, 
            ISpreadsheetEntityMapper<TimeDto, SeasonTimeOfDay> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_TIMES;
        }

        protected override void WriteToDatabase(IEnumerable<SeasonTimeOfDay> entities)
        {
            _dbContext.SeasonTimesOfDay.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
