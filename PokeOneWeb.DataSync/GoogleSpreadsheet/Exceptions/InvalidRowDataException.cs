using System;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions
{
    public class InvalidRowDataException : Exception
    {
        public InvalidRowDataException(string message) : base(message)
        {
        }
    }
}