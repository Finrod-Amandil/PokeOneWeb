﻿namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Abilities
{
    public class AbilitySheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public string PokeApiName { get; set; }
        public string ShortEffect { get; set; }
        public string Effect { get; set; }
        public decimal AtkBoost { get; set; }
        public decimal SpaBoost { get; set; }
        public decimal DefBoost { get; set; }
        public decimal SpdBoost { get; set; }
        public decimal SpeBoost { get; set; }
        public decimal HpBoost { get; set; }
        public string BoostConditions { get; set; }
    }
}
