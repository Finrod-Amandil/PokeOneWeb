using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Abilities
{
    public class AbilityImporter : SpreadsheetEntityImporter<AbilityDto, Ability>
    {
        private readonly ApplicationDbContext _dbContext;

        public AbilityImporter(
            ISpreadsheetEntityReader<AbilityDto> reader, 
            ISpreadsheetEntityMapper<AbilityDto, Ability> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_ABILITIES;
        }

        protected override void WriteToDatabase(IEnumerable<Ability> entities)
        {
            _dbContext.Abilities.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
