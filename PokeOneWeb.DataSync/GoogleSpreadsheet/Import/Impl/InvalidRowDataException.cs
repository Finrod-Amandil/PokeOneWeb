namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class InvalidRowDataException : Exception
    {
        public InvalidRowDataException(string message) : base(message) { }
    }
}
