namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes
{
    public class SpawnTypeSheetDto : XISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int SortIndex { get; set; }

        public bool IsSyncable { get; set; }

        public bool IsInfinite { get; set; }

        public string Color { get; set; }
    }
}