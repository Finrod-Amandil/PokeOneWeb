namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureSheetDto : XISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int Attack { get; set; }

        public int SpecialAttack { get; set; }

        public int Defense { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }
    }
}