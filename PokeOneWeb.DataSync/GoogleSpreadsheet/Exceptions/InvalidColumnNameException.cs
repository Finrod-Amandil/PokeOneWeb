using System;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Exceptions
{
    public class InvalidColumnNameException : Exception
    {
        public InvalidColumnNameException(string columnName) : base($"Invalid column name: {columnName}")
        {
        }
    }
}