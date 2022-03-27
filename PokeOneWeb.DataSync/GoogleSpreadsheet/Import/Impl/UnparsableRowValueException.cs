namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class UnparsableRowValueException : InvalidRowDataException
    {
        public UnparsableRowValueException(object value, string type) :
            base($"Value could not be parsed as {type}: {value}")
        {
        }
    }
}