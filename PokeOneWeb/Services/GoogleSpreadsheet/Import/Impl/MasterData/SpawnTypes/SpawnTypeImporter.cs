using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes
{
    public class SpawnTypeImporter : SpreadsheetEntityImporter<SpawnTypeDto, SpawnType>
    {
        private readonly ApplicationDbContext _dbContext;

        public SpawnTypeImporter(
            ISpreadsheetEntityReader<SpawnTypeDto> reader, 
            ISpreadsheetEntityMapper<SpawnTypeDto, SpawnType> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_SPAWN_TYPES;
        }

        protected override void WriteToDatabase(IEnumerable<SpawnType> entities)
        {
            _dbContext.SpawnTypes.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
