using System;

namespace PokeOneWeb.Data.Attributes
{
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