using System;

namespace PokeOneWeb.Data.Attributes
{
    /// <summary>
    /// Attribute to associate entity classes with the google sheet name/prefix from which
    /// they are being imported.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SheetAttribute : Attribute
    {
        public string SheetName { get; }

        public SheetAttribute(string sheetName)
        {
            SheetName = sheetName;
        }
    }
}