namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Natures
{
    public class NatureSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int Attack { get; set; }

        public int SpecialAttack { get; set; }

        public int Defense { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }
    }
}
