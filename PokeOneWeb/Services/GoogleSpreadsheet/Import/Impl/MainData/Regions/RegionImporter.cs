using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.Regions
{
    public class RegionImporter : SpreadsheetEntityImporter<RegionDto, Region>
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionImporter(
            ISpreadsheetEntityReader<RegionDto> reader, 
            ISpreadsheetEntityMapper<RegionDto, Region> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_REGIONS;
        }

        protected override void WriteToDatabase(IEnumerable<Region> entities)
        {
            _dbContext.Regions.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
