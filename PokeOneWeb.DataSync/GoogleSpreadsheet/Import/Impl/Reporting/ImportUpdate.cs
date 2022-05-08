using PokeOneWeb.Data;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class ImportUpdate
    {
        public string EntityName { get; set; }

        public DbAction DbAction { get; set; }

        public string Hash { get; set; }

        public int ApplicationDbId { get; set; }
    }
}