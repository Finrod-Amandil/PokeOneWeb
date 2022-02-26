namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves
{
    public class MoveTutorMoveSheetDto : ISpreadsheetEntityDto
    {
        public string MoveTutorName { get; set; }
        public string MoveName { get; set; }
        public int RedShardPrice { get; set; }
        public int BlueShardPrice { get; set; }
        public int GreenShardPrice { get; set; }
        public int YellowShardPrice { get; set; }
        public int PWTBPPrice { get; set; }
        public int BFBPPrice { get; set; }
        public int PokeDollarPrice { get; set; }
        public int PokeGoldPrice { get; set; }
    }
}
