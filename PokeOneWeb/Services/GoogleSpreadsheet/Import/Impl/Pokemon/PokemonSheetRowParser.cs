using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Pokemon
{
    public class PokemonSheetRowParser : ISheetRowParser<PokemonSheetDto>
    {
        public PokemonSheetDto ReadRow(List<object> values)
        {
            if (values is null || values.Count < 33)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PokemonSheetDto
            {
                SortIndex = int.TryParse(values[0].ToString(), out var parsedSortIndex) ? parsedSortIndex : 0,
                PokedexNumber = int.TryParse(values[1].ToString(), out var parsedPokedexNumber) ? parsedPokedexNumber : 0,
                PokemonSpeciesName = values[2] as string,
                DefaultVarietyName = values[3] as string,
                PokemonVarietyName = values[4] as string,
                ResourceName = values[5] as string,
                PokemonFormName = values[6] as string,
                AvailabilityName = values[7] as string,
                SpriteName = values[8] as string,
                DoInclude = bool.TryParse(values[9].ToString(), out var parsedDoInclude) && parsedDoInclude,
                DefaultFormName = values[10] as string,

                Attack = int.TryParse(values[11].ToString(), out var parsedAtk) ? parsedAtk : 0,
                SpecialAttack = int.TryParse(values[12].ToString(), out var parsedSpa) ? parsedSpa : 0,
                Defense = int.TryParse(values[13].ToString(), out var parsedDef) ? parsedDef : 0,
                SpecialDefense = int.TryParse(values[14].ToString(), out var parsedSpd) ? parsedSpd : 0,
                Speed = int.TryParse(values[15].ToString(), out var parsedSpe) ? parsedSpe : 0,
                HitPoints = int.TryParse(values[16].ToString(), out var parsedHp) ? parsedHp : 0,

                AttackEvYield = int.TryParse(values[17].ToString(), out var parsedAtkEv) ? parsedAtkEv : 0,
                SpecialAttackEvYield = int.TryParse(values[18].ToString(), out var parsedSpaEv) ? parsedSpaEv : 0,
                DefenseEvYield = int.TryParse(values[19].ToString(), out var parsedDefEv) ? parsedDefEv : 0,
                SpecialDefenseEvYield = int.TryParse(values[20].ToString(), out var parsedSpdEv) ? parsedSpdEv : 0,
                SpeedEvYield = int.TryParse(values[21].ToString(), out var parsedSpeEv) ? parsedSpeEv : 0,
                HitPointEvYield = int.TryParse(values[22].ToString(), out var parsedHpEv) ? parsedHpEv : 0,

                Type1Name = values[23] as string,
                Type2Name = values[24] as string,
                PrimaryAbilityName = values[25] as string,
                SecondaryAbilityName = values[26] as string,
                HiddenAbilityName = values[27] as string,
                PvpTierName = values[28] as string,
                IsMega = bool.TryParse(values[29].ToString(), out var parsedIsMega) && parsedIsMega,
                IsFullyEvolved = bool.TryParse(values[30].ToString(), out var parsedIsFullyEvolved) && parsedIsFullyEvolved,
                Generation = int.TryParse(values[31].ToString(), out var parsedGeneration) ? parsedGeneration : 0,
                CatchRate = int.TryParse(values[32].ToString(), out var parsedCatchRate) ? parsedCatchRate : 0,
                HasGender = !bool.TryParse(values[33].ToString(), out var parsedHasGender) || parsedHasGender,
                MaleRatio = decimal.TryParse(values[34].ToString(), out var parsedMaleRatio) ? parsedMaleRatio : 0.5M,
                EggCycles = int.TryParse(values[35].ToString(), out var parsedEggCycles) ? parsedEggCycles : 0,
                Height = decimal.TryParse(values[36].ToString(), out var parsedHeight) ? parsedHeight : 0,
                Weight = decimal.TryParse(values[37].ToString(), out var parsedWeight) ? parsedWeight : 0,
                ExpYield = int.TryParse(values[38].ToString(), out var parsedExpYield) ? parsedExpYield : 0,
            };

            if (value.PokedexNumber == 0)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.PokedexNumber)} was zero.");
            }

            if (value.PokemonSpeciesName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.PokemonSpeciesName)} was empty.");
            }

            if (value.PokemonVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (value.PokemonFormName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.PokemonFormName)} was empty.");
            }

            if (value.ResourceName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.ResourceName)} was empty.");
            }

            if (value.AvailabilityName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.AvailabilityName)} was empty.");
            }

            if (value.DefaultVarietyName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.DefaultVarietyName)} was empty.");
            }

            if (value.DefaultFormName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.DefaultFormName)} was empty.");
            }

            if (value.Type1Name is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.Type1Name)} was empty.");
            }

            if (value.PrimaryAbilityName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.PrimaryAbilityName)} was empty.");
            }

            if (value.PvpTierName is null)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.PvpTierName)} was empty.");
            }

            if (value.Generation == 0)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.Generation)} was zero.");
            }

            if (value.CatchRate == 0)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.CatchRate)} was zero.");
            }

            if (value.EggCycles == 0)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.EggCycles)} was zero.");
            }

            if (value.Height == 0)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.Height)} was zero.");
            }

            if (value.Weight == 0)
            {
                throw new InvalidRowDataException($"Tried to read Pokemon, but required field {nameof(value.Weight)} was zero.");
            }

            if (values.Count > 39)
            {
                value.SmogonUrl = values[39] as string;
            }

            if (values.Count > 40)
            {
                value.BulbapediaUrl = values[40] as string;
            }

            if (values.Count > 41)
            {
                value.PokeoneCommunityUrl = values[41] as string;
            }

            if (values.Count > 42)
            {
                value.PokemonShowdownUrl = values[42] as string;
            }

            if (values.Count > 43)
            {
                value.SerebiiUrl = values[43] as string;
            }

            if (values.Count > 44)
            {
                value.PokemonDbUrl = values[44] as string;
            }

            if (values.Count > 45)
            {
                value.PokemonSpeciesPokeApiName = values[45] as string;
            }

            if (values.Count > 46)
            {
                value.PokemonVarietyPokeApiName = values[46] as string;
            }

            if (values.Count > 47)
            {
                value.PokemonFormPokeApiName = values[47] as string;
            }

            if (values.Count > 48)
            {
                value.Notes = values[48] as string;
            }

            return value;
        }
    }
}
