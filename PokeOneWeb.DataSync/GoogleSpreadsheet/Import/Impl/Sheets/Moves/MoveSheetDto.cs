namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Moves
{
    public class MoveSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }

        public bool DoInclude { get; set; }

        public string ResourceName { get; set; }

        public string DamageClassName { get; set; }

        public string TypeName { get; set; }

        public int AttackPower { get; set; }

        public int Accuracy { get; set; }

        public int PowerPoints { get; set; }

        public int Priority { get; set; }

        public string Effect { get; set; }
    }
}
