using System;
using PokeOneWeb.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class ImportError
    {
        public Entity Entity { get; set; }

        public string Hash { get; set; }

        public string Message { get; set; }
    }
}
