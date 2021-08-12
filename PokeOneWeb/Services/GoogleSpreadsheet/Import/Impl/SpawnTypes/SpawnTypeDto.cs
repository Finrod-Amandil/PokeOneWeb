namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SpawnTypes
{
    public class SpawnTypeDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int SortIndex { get; set; }

        public bool IsSyncable { get; set; }

        public bool IsInfinite { get; set; }

        public string Color { get; set; }
    }
}
