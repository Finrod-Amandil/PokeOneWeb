using System;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.Location
{
    public class LocationMapper : ISpreadsheetMapper<LocationDto, Data.Entities.Location>
    {
        public IEnumerable<Data.Entities.Location> Map(IEnumerable<LocationDto> dtos)
        {
            throw new NotImplementedException();
        }
    }
}
