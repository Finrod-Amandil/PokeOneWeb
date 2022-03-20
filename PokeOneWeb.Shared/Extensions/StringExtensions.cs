using System;

namespace PokeOneWeb.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsExact(this string str, string other)
        {
            return str.Equals(other, StringComparison.Ordinal);
        }

        public static bool EqualsExact2(this string str, string other)
        {
            return str.Equals(other, StringComparison.Ordinal);
        }
    }
}
