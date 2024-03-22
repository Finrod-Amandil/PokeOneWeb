namespace PokeOneWeb.DataSync.Import.Reporting
{
    public class ImportError
    {
        public string EntityName { get; set; }

        public string Hash { get; set; }

        public string Message { get; set; }
    }
}