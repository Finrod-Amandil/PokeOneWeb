using System;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions
{
    /// <summary>
    /// Exception that can be used, if a row of sheet data is erroneous and can not be
    /// processed.
    /// </summary>
    public class InvalidRowDataException : Exception
    {
        public InvalidRowDataException(string message) : base(message)
        {
        }
    }
}