using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Items
{
    public class ItemImporter : SpreadsheetEntityImporter<ItemDto, Item>
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemImporter(
            ISpreadsheetEntityReader<ItemDto> reader, 
            ISpreadsheetEntityMapper<ItemDto, Item> mapper,
            ApplicationDbContext dbContext) : base(reader, mapper)
        {
            _dbContext = dbContext;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_ITEMS;
        }

        protected override void WriteToDatabase(IEnumerable<Item> entities)
        {
            _dbContext.Items.AddRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
