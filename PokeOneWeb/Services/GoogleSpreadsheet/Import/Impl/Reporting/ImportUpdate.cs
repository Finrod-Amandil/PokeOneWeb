using PokeOneWeb.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class ImportUpdate
    {
        public Entity Entity { get; set; }

        public DbAction DbAction { get; set; }

        public string Hash { get; set; }

        public int ApplicationDbId { get; set; }
    }
}
