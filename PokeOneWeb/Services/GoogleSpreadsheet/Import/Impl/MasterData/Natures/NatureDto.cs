namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Natures
{
    public class NatureDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public int Attack { get; set; }

        public int SpecialAttack { get; set; }

        public int Defense { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }
    }
}
