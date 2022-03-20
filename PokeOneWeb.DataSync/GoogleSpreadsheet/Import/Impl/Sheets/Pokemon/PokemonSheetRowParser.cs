namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon
{
    public class PokemonSheetRowParser : SheetRowParser<PokemonSheetDto>
    {
        protected override int RequiredValueCount => 39;

        protected override List<Action<PokemonSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.SortIndex = ParseAsInt(value),
            (dto, value) => dto.PokedexNumber = ParseAsInt(value),
            (dto, value) => dto.PokemonSpeciesName = ParseAsNonEmptyString(value),
            (dto, value) => dto.DefaultVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.PokemonVarietyName = ParseAsNonEmptyString(value),
            (dto, value) => dto.ResourceName = ParseAsNonEmptyString(value),
            (dto, value) => dto.PokemonFormName = ParseAsNonEmptyString(value),
            (dto, value) => dto.AvailabilityName = ParseAsNonEmptyString(value),
            (dto, value) => dto.SpriteName = ParseAsNonEmptyString(value),
            (dto, value) => dto.DoInclude = ParseAsBoolean(value),
            (dto, value) => dto.DefaultFormName = ParseAsNonEmptyString(value),

            (dto, value) => dto.Attack = ParseAsInt(value),
            (dto, value) => dto.SpecialAttack = ParseAsInt(value),
            (dto, value) => dto.Defense = ParseAsInt(value),
            (dto, value) => dto.SpecialDefense = ParseAsInt(value),
            (dto, value) => dto.Speed = ParseAsInt(value),
            (dto, value) => dto.HitPoints = ParseAsInt(value),

            (dto, value) => dto.AttackEvYield = ParseAsInt(value),
            (dto, value) => dto.SpecialAttackEvYield = ParseAsInt(value),
            (dto, value) => dto.DefenseEvYield = ParseAsInt(value),
            (dto, value) => dto.SpecialDefenseEvYield = ParseAsInt(value),
            (dto, value) => dto.SpeedEvYield = ParseAsInt(value),
            (dto, value) => dto.HitPointEvYield = ParseAsInt(value),

            (dto, value) => dto.Type1Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.Type2Name = ParseAsString(value),
            (dto, value) => dto.PrimaryAbilityName = ParseAsNonEmptyString(value),
            (dto, value) => dto.SecondaryAbilityName = ParseAsString(value),
            (dto, value) => dto.HiddenAbilityName = ParseAsString(value),
            (dto, value) => dto.PvpTierName = ParseAsString(value),
            (dto, value) => dto.IsMega = ParseAsBoolean(value),
            (dto, value) => dto.IsFullyEvolved = ParseAsBoolean(value),
            (dto, value) => dto.Generation = ParseAsInt(value),
            (dto, value) => dto.CatchRate = ParseAsInt(value),
            (dto, value) => dto.HasGender = ParseAsBoolean(value),
            (dto, value) => dto.MaleRatio = ParseAsDecimal(value),
            (dto, value) => dto.EggCycles = ParseAsInt(value),
            (dto, value) => dto.Height = ParseAsDecimal(value),
            (dto, value) => dto.Weight = ParseAsDecimal(value),
            (dto, value) => dto.ExpYield = ParseAsInt(value),

            (dto, value) => dto.SmogonUrl = ParseAsString(value),
            (dto, value) => dto.BulbapediaUrl = ParseAsString(value),
            (dto, value) => dto.PokeoneCommunityUrl = ParseAsString(value),
            (dto, value) => dto.PokemonShowdownUrl = ParseAsString(value),
            (dto, value) => dto.SerebiiUrl = ParseAsString(value),
            (dto, value) => dto.PokemonDbUrl = ParseAsString(value),

            (dto, value) => dto.Notes = ParseAsString(value),
        };
    }
}
