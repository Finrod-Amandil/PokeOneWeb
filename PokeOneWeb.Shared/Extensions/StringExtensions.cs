using System;

namespace PokeOneWeb.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsExact(this string str, string other)
        {
            return str.Equals(other, StringComparison.Ordinal);
        }
    }
}
