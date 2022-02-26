using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Abilities
{
    public class AbilityMapper : SpreadsheetEntityMapper<AbilitySheetDto, Ability>
    {
        public AbilityMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.Ability;

        protected override bool IsValid(AbilitySheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(AbilitySheetDto dto)
        {
            return dto.Name;
        }

        protected override Ability MapEntity(AbilitySheetDto dto, RowHash rowHash, Ability ability = null)
        {
            ability ??= new Ability();

            ability.IdHash = rowHash.IdHash;
            ability.Hash = rowHash.ContentHash;
            ability.ImportSheetId = rowHash.ImportSheetId;
            ability.PokeApiName = dto.PokeApiName;
            ability.Name = dto.Name;
            ability.EffectDescription = dto.Effect;
            ability.EffectShortDescription = dto.ShortEffect;

            ability.AttackBoost = dto.AtkBoost;
            ability.SpecialAttackBoost = dto.SpaBoost;
            ability.DefenseBoost = dto.DefBoost;
            ability.SpecialDefenseBoost = dto.SpdBoost;
            ability.SpeedBoost = dto.SpeBoost;
            ability.HitPointsBoost = dto.HpBoost;
            ability.BoostConditions = dto.BoostConditions;

            return ability;
        }
    }
}
