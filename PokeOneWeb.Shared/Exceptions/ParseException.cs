using System;

namespace PokeOneWeb.Shared.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(object value, string type)
            : base($"Value could not be parsed as {type}: {value}")
        {
        }
    }
}