namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostSheetDto : XISpreadsheetEntityDto
    {
        public string ItemName { get; set; }
        public decimal AtkBoost { get; set; }
        public decimal SpaBoost { get; set; }
        public decimal DefBoost { get; set; }
        public decimal SpdBoost { get; set; }
        public decimal SpeBoost { get; set; }
        public decimal HpBoost { get; set; }
        public string RequiredPokemonName { get; set; }
    }
}