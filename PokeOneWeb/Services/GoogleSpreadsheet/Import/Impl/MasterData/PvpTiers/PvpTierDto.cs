namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.PvpTiers
{
    public class PvpTierDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int SortIndex { get; set; }
    }
}
