namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypeRelations
{
    public class ElementalTypeRelationDto : ISpreadsheetEntityDto
    {
        public string AttackingTypeName { get; set; }
        public string DefendingTypeName { get; set; }

        public decimal Effectivity { get; set; }
    }
}
