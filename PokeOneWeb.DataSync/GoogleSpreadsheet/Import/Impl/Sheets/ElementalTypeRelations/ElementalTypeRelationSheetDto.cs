namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations
{
    public class ElementalTypeRelationSheetDto : ISpreadsheetEntityDto
    {
        public string AttackingTypeName { get; set; }

        public string DefendingTypeName { get; set; }

        public decimal Effectivity { get; set; }
    }
}