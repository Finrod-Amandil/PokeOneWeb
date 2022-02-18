namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostSheetDto : ISpreadsheetEntityDto
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
