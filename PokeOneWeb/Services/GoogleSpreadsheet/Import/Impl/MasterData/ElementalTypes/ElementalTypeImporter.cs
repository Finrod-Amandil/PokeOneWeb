using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ElementalTypes
{
    public class ElementalTypeImporter : SpreadsheetEntityImporter<ElementalTypeDto, ElementalType>
    {
        private readonly ApplicationDbContext _dbContext;

        public ElementalTypeImporter(
            ISpreadsheetEntityReader<ElementalTypeDto> reader, 
            ISpreadsheetEntityMapper<ElementalTypeDto, ElementalType> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_ELEMENTAL_TYPES;
        }

        protected override void WriteToDatabase(IEnumerable<ElementalType> entities)
        {
            var elementalTypes = entities.ToList();

            _dbContext.ElementalTypes.AddRange(elementalTypes);
            _dbContext.SaveChanges();
        }
    }
}
