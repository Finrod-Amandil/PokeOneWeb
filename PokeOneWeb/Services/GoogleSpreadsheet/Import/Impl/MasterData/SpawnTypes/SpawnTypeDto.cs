namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.SpawnTypes
{
    public class SpawnTypeDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public bool IsSyncable { get; set; }

        public bool IsInfinite { get; set; }
    }
}
