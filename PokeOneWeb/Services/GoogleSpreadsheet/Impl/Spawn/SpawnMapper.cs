using System;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.Spawn
{
    public class SpawnMapper : ISpreadsheetMapper<SpawnDto, Data.Entities.Spawn>
    {
        public IEnumerable<Data.Entities.Spawn> Map(IEnumerable<SpawnDto> dtos)
        {
            throw new NotImplementedException();
        }
    }
}
