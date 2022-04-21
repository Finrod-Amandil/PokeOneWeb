﻿namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon
{
    public class PokemonSheetDto : XISpreadsheetEntityDto
    {
        public int SortIndex { get; set; }

        public int PokedexNumber { get; set; }

        public string PokemonSpeciesName { get; set; }

        public string DefaultVarietyName { get; set; }

        public string PokemonVarietyName { get; set; }

        public string ResourceName { get; set; }

        public string PokemonFormName { get; set; }

        public string AvailabilityName { get; set; }

        public string SpriteName { get; set; }

        public bool DoInclude { get; set; }

        public string DefaultFormName { get; set; }

        public int Attack { get; set; }

        public int SpecialAttack { get; set; }

        public int Defense { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }

        public int HitPoints { get; set; }

        public int AttackEvYield { get; set; }

        public int SpecialAttackEvYield { get; set; }

        public int DefenseEvYield { get; set; }

        public int SpecialDefenseEvYield { get; set; }

        public int SpeedEvYield { get; set; }

        public int HitPointEvYield { get; set; }

        public string Type1Name { get; set; }

        public string Type2Name { get; set; }

        public string PrimaryAbilityName { get; set; }

        public string SecondaryAbilityName { get; set; }

        public string HiddenAbilityName { get; set; }

        public string PvpTierName { get; set; }

        public bool IsMega { get; set; }

        public bool IsFullyEvolved { get; set; }

        public int Generation { get; set; }

        public int CatchRate { get; set; }

        public bool HasGender { get; set; }

        public decimal MaleRatio { get; set; }

        public int EggCycles { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public int ExpYield { get; set; }

        public string SmogonUrl { get; set; }

        public string BulbapediaUrl { get; set; }

        public string PokeoneCommunityUrl { get; set; }

        public string PokemonShowdownUrl { get; set; }

        public string SerebiiUrl { get; set; }

        public string PokemonDbUrl { get; set; }

        public string Notes { get; set; }
    }
}