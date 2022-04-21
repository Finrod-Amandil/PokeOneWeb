using System;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SheetNameAttribute : Attribute
    {
        public string SheetName { get; }

        public SheetNameAttribute(string sheetName)
        {
            SheetName = sheetName;
        }
    }
}