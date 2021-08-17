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
                CatchRate = int.TryParse(values[32].ToString(), out var parsedCatchRate) ? parsedCatchRate : 0
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

            if (values.Count > 33)
            {
                value.SmogonUrl = values[33] as string;
            }

            if (values.Count > 34)
            {
                value.BulbapediaUrl = values[34] as string;
            }

            if (values.Count > 35)
            {
                value.PokeoneCommunityUrl = values[35] as string;
            }

            if (values.Count > 36)
            {
                value.PokemonShowdownUrl = values[36] as string;
            }

            if (values.Count > 37)
            {
                value.SerebiiUrl = values[37] as string;
            }

            if (values.Count > 38)
            {
                value.PokemonDbUrl = values[38] as string;
            }

            if (values.Count > 39)
            {
                value.PokemonSpeciesPokeApiName = values[39] as string;
            }

            if (values.Count > 40)
            {
                value.PokemonVarietyPokeApiName = values[40] as string;
            }

            if (values.Count > 41)
            {
                value.PokemonFormPokeApiName = values[41] as string;
            }

            if (values.Count > 42)
            {
                value.Notes = values[42] as string;
            }

            return value;
        }
    }
}
