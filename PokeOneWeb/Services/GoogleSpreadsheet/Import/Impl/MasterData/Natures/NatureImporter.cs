using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Natures
{
    public class NatureImporter : SpreadsheetEntityImporter<NatureDto, Nature>
    {
        private readonly ApplicationDbContext _dbContext;

        public NatureImporter(
            ISpreadsheetEntityReader<NatureDto> reader, 
            ISpreadsheetEntityMapper<NatureDto, Nature> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_NATURES;
        }

        protected override void WriteToDatabase(IEnumerable<Nature> entities)
        {
            _dbContext.Natures.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
