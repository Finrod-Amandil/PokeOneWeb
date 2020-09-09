using System;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl
{
    public class InvalidRowDataException : Exception
    {
        public InvalidRowDataException(string message) : base(message) { }
    }
}
