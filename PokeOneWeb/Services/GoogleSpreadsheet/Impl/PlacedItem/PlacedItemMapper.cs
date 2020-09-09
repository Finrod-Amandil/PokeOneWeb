using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.PlacedItem
{
    public class PlacedItemMapper : ISpreadsheetMapper<PlacedItemDto, Data.Entities.PlacedItem>
    {
        public IEnumerable<Data.Entities.PlacedItem> Map(IEnumerable<PlacedItemDto> dtos)
        {
            throw new System.NotImplementedException();
        }
    }
}
