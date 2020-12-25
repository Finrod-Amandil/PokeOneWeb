using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Pokemon
{
    public class PokemonReader : SpreadsheetEntityReader<PokemonDto>
    {
        public PokemonReader(ILogger<PokemonReader> logger) : base(logger) { }

        protected override PokemonDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 31)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new PokemonDto
            {
                PokedexNumber = (int?) rowData.Values[0]?.EffectiveValue?.NumberValue ?? 0,
                PokemonSpeciesName = rowData.Values[1]?.EffectiveValue?.StringValue,
                PokemonVarietyName = rowData.Values[2]?.EffectiveValue?.StringValue,
                PokemonFormName = rowData.Values[3]?.EffectiveValue?.StringValue,
                ResourceName = rowData.Values[4]?.EffectiveValue?.StringValue,
                DoInclude = rowData.Values[5]?.EffectiveValue?.BoolValue ?? false,
                AvailabilityName = rowData.Values[6]?.EffectiveValue?.StringValue,
                DefaultVarietyName = rowData.Values[7]?.EffectiveValue?.StringValue,
                DefaultFormName = rowData.Values[8]?.EffectiveValue?.StringValue,

                Attack = (int?) rowData.Values[9]?.EffectiveValue?.NumberValue ?? 0,
                SpecialAttack = (int?) rowData.Values[10]?.EffectiveValue?.NumberValue ?? 0,
                Defense = (int?) rowData.Values[11]?.EffectiveValue?.NumberValue ?? 0,
                SpecialDefense = (int?) rowData.Values[12]?.EffectiveValue?.NumberValue ?? 0,
                Speed = (int?) rowData.Values[13]?.EffectiveValue?.NumberValue ?? 0,
                HitPoints = (int?) rowData.Values[14]?.EffectiveValue?.NumberValue ?? 0,

                AttackEvYield = (int?) rowData.Values[15]?.EffectiveValue?.NumberValue ?? 0,
                SpecialAttackEvYield = (int?) rowData.Values[16]?.EffectiveValue?.NumberValue ?? 0,
                DefenseEvYield = (int?) rowData.Values[17]?.EffectiveValue?.NumberValue ?? 0,
                SpecialDefenseEvYield = (int?) rowData.Values[18]?.EffectiveValue?.NumberValue ?? 0,
                SpeedEvYield = (int?) rowData.Values[19]?.EffectiveValue?.NumberValue ?? 0,
                HitPointEvYield = (int?) rowData.Values[20]?.EffectiveValue?.NumberValue ?? 0,

                Type1Name = rowData.Values[21]?.EffectiveValue?.StringValue,
                Type2Name = rowData.Values[22]?.EffectiveValue?.StringValue,
                PrimaryAbilityName = rowData.Values[23]?.EffectiveValue?.StringValue,
                SecondaryAbilityName = rowData.Values[24]?.EffectiveValue?.StringValue,
                HiddenAbilityName = rowData.Values[25]?.EffectiveValue?.StringValue,
                PvpTierName = rowData.Values[26]?.EffectiveValue?.StringValue,
                IsMega = rowData.Values[27]?.EffectiveValue?.BoolValue ?? false,
                IsFullyEvolved = rowData.Values[28]?.EffectiveValue?.BoolValue ?? false,
                Generation = (int?) rowData.Values[29]?.EffectiveValue?.NumberValue ?? 0,
                CatchRate = (int?) rowData.Values[30]?.EffectiveValue?.NumberValue ?? 0
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

            if (rowData.Values.Count > 31)
            {
                value.SmogonUrl = rowData.Values[31]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 32)
            {
                value.BulbapediaUrl = rowData.Values[32]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 33)
            {
                value.PokeoneCommunityUrl = rowData.Values[33]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 34)
            {
                value.PokemonShowdownUrl = rowData.Values[34]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 35)
            {
                value.SerebiiUrl = rowData.Values[35]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 36)
            {
                value.PokemonDbUrl = rowData.Values[36]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 37)
            {
                value.SpriteName = rowData.Values[37]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 38)
            {
                value.PokemonSpeciesPokeApiName = rowData.Values[38]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 39)
            {
                value.PokemonVarietyPokeApiName = rowData.Values[39]?.EffectiveValue?.StringValue;
            }

            if (rowData.Values.Count > 40)
            {
                value.PokemonFormPokeApiName = rowData.Values[40]?.EffectiveValue?.StringValue;
            }

            return value;
        }
    }
}
