using FluentAssertions;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon
{
    public class PokemonSheetRowParserTest
    {
        [Fact]
        public void ReadRow_WithMinimalValidValues_ShouldParse()
        {
            // Arrange
            var parser = new PokemonSheetRowParser();

            var sortIndex = 0;
            var pokedexNumber = 1;
            var pokemonSpeciesName = "Pokemon Species Name";
            var defaultVarietyName = "Default Variety Name";
            var pokemonVarietyName = "Pokemon Variety Name";
            var resourceName = "Resource Name";
            var pokemonFormName = "Pokemon Form Name";
            var availabilityName = "Availability Name";
            var spriteName = "sprite-name.png";
            var doInclude = true;
            var defaultFormName = "Default Form Name";

            var attack = 10;
            var specialAttack = 11;
            var defense = 12;
            var specialDefense = 13;
            var speed = 14;
            var hitPoints = 15;

            var attackEvYield = 16;
            var specialAttackEvYield = 17;
            var defenseEvYield = 18;
            var specialDefenseEvYield = 19;
            var speedEvYield = 20;
            var hitPointsEvYield = 21;

            var type1Name = "Type 1 Name";
            var type2Name = "Type 2 Name";
            var primaryAbilityName = "Primary Ability Name";
            var secondaryAbilityName = "Secondary Ability Name";
            var hiddenAbilityName = "Hidden Ability Name";
            var pvpTierName = "PvP Tier Name";
            var isMega = false;
            var isFullyEvolved = true;
            var generation = 7;
            var catchRate = 8;
            var hasGender = true;
            var maleRatio = 0.5M;
            var eggCycles = 1000;
            var height = 10.5M;
            var weight = 11.5M;
            var expYield = 300;

            var values = new List<object>
            {
                sortIndex,
                pokedexNumber,
                pokemonSpeciesName,
                defaultVarietyName,
                pokemonVarietyName,
                resourceName,
                pokemonFormName,
                availabilityName,
                spriteName,
                doInclude,
                defaultFormName,

                attack,
                specialAttack,
                defense,
                specialDefense,
                speed,
                hitPoints,

                attackEvYield,
                specialAttackEvYield,
                defenseEvYield,
                specialDefenseEvYield,
                speedEvYield,
                hitPointsEvYield,

                type1Name,
                type2Name,
                primaryAbilityName,
                secondaryAbilityName,
                hiddenAbilityName,
                pvpTierName,
                isMega,
                isFullyEvolved,
                generation,
                catchRate,
                hasGender,
                maleRatio,
                eggCycles,
                height,
                weight,
                expYield
            };

            var expected = new PokemonSheetDto
            {
                SortIndex = sortIndex,
                PokedexNumber = pokedexNumber,
                PokemonSpeciesName = pokemonSpeciesName,
                DefaultVarietyName = defaultVarietyName,
                PokemonVarietyName = pokemonVarietyName,
                ResourceName = resourceName,
                PokemonFormName = pokemonFormName,
                AvailabilityName = availabilityName,
                SpriteName = spriteName,
                DoInclude = doInclude,
                DefaultFormName = defaultFormName,

                Attack = attack,
                SpecialAttack = specialAttack,
                Defense = defense,
                SpecialDefense = specialDefense,
                Speed = speed,
                HitPoints = hitPoints,

                AttackEvYield = attackEvYield,
                SpecialAttackEvYield = specialAttackEvYield,
                DefenseEvYield = defenseEvYield,
                SpecialDefenseEvYield = specialDefenseEvYield,
                SpeedEvYield = speedEvYield,
                HitPointEvYield = hitPointsEvYield,

                Type1Name = type1Name,
                Type2Name = type2Name,
                PrimaryAbilityName = primaryAbilityName,
                SecondaryAbilityName = secondaryAbilityName,
                HiddenAbilityName = hiddenAbilityName,
                PvpTierName = pvpTierName,
                IsMega = isMega,
                IsFullyEvolved = isFullyEvolved,
                Generation = generation,
                CatchRate = catchRate,
                HasGender = hasGender,
                MaleRatio = maleRatio,
                EggCycles = eggCycles,
                Height = height,
                Weight = weight,
                ExpYield = expYield
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithAllValidValues_ShouldParse()
        {
            // Arrange
            var parser = new PokemonSheetRowParser();

            var sortIndex = 0;
            var pokedexNumber = 1;
            var pokemonSpeciesName = "Pokemon Species Name";
            var defaultVarietyName = "Default Variety Name";
            var pokemonVarietyName = "Pokemon Variety Name";
            var resourceName = "Resource Name";
            var pokemonFormName = "Pokemon Form Name";
            var availabilityName = "Availability Name";
            var spriteName = "sprite-name.png";
            var doInclude = true;
            var defaultFormName = "Default Form Name";

            var attack = 10;
            var specialAttack = 11;
            var defense = 12;
            var specialDefense = 13;
            var speed = 14;
            var hitPoints = 15;

            var attackEvYield = 16;
            var specialAttackEvYield = 17;
            var defenseEvYield = 18;
            var specialDefenseEvYield = 19;
            var speedEvYield = 20;
            var hitPointsEvYield = 21;

            var type1Name = "Type 1 Name";
            var type2Name = "Type 2 Name";
            var primaryAbilityName = "Primary Ability Name";
            var secondaryAbilityName = "Secondary Ability Name";
            var hiddenAbilityName = "Hidden Ability Name";
            var pvpTierName = "PvP Tier Name";
            var isMega = false;
            var isFullyEvolved = true;
            var generation = 7;
            var catchRate = 8;
            var hasGender = true;
            var maleRatio = 0.5M;
            var eggCycles = 1000;
            var height = 10.5M;
            var weight = 11.5M;
            var expYield = 300;

            var smogonUrl = "Smogon-Url";
            var bulbapediaUrl = "Bulbapedia-Url";
            var pokeoneCommunityUrl = "PokeoneCommunity-Url";
            var pokemonShowdownUrl = "Pokemon-Showdown-Url";
            var serebiiUrl = "Serebii-Url";
            var pokemonDbUrl = "Pokemon-Db-Url";

            var notes = "Notes";

            var values = new List<object>
            {
                sortIndex,
                pokedexNumber,
                pokemonSpeciesName,
                defaultVarietyName,
                pokemonVarietyName,
                resourceName,
                pokemonFormName,
                availabilityName,
                spriteName,
                doInclude,
                defaultFormName,

                attack,
                specialAttack,
                defense,
                specialDefense,
                speed,
                hitPoints,

                attackEvYield,
                specialAttackEvYield,
                defenseEvYield,
                specialDefenseEvYield,
                speedEvYield,
                hitPointsEvYield,

                type1Name,
                type2Name,
                primaryAbilityName,
                secondaryAbilityName,
                hiddenAbilityName,
                pvpTierName,
                isMega,
                isFullyEvolved,
                generation,
                catchRate,
                hasGender,
                maleRatio,
                eggCycles,
                height,
                weight,
                expYield,

                smogonUrl,
                bulbapediaUrl,
                pokeoneCommunityUrl,
                pokemonShowdownUrl,
                serebiiUrl,
                pokemonDbUrl,

                notes
            };

            var expected = new PokemonSheetDto
            {
                SortIndex = sortIndex,
                PokedexNumber = pokedexNumber,
                PokemonSpeciesName = pokemonSpeciesName,
                DefaultVarietyName = defaultVarietyName,
                PokemonVarietyName = pokemonVarietyName,
                ResourceName = resourceName,
                PokemonFormName = pokemonFormName,
                AvailabilityName = availabilityName,
                SpriteName = spriteName,
                DoInclude = doInclude,
                DefaultFormName = defaultFormName,

                Attack = attack,
                SpecialAttack = specialAttack,
                Defense = defense,
                SpecialDefense = specialDefense,
                Speed = speed,
                HitPoints = hitPoints,

                AttackEvYield = attackEvYield,
                SpecialAttackEvYield = specialAttackEvYield,
                DefenseEvYield = defenseEvYield,
                SpecialDefenseEvYield = specialDefenseEvYield,
                SpeedEvYield = speedEvYield,
                HitPointEvYield = hitPointsEvYield,

                Type1Name = type1Name,
                Type2Name = type2Name,
                PrimaryAbilityName = primaryAbilityName,
                SecondaryAbilityName = secondaryAbilityName,
                HiddenAbilityName = hiddenAbilityName,
                PvpTierName = pvpTierName,
                IsMega = isMega,
                IsFullyEvolved = isFullyEvolved,
                Generation = generation,
                CatchRate = catchRate,
                HasGender = hasGender,
                MaleRatio = maleRatio,
                EggCycles = eggCycles,
                Height = height,
                Weight = weight,
                ExpYield = expYield,

                SmogonUrl = smogonUrl,
                BulbapediaUrl = bulbapediaUrl,
                PokeoneCommunityUrl = pokeoneCommunityUrl,
                PokemonShowdownUrl = pokemonShowdownUrl,
                SerebiiUrl = serebiiUrl,
                PokemonDbUrl = pokemonDbUrl,

                Notes = notes
            };

            // Act
            var result = parser.ReadRow(values);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReadRow_WithInsufficientValues_ShouldThrow()
        {
            // Arrange
            var parser = new PokemonSheetRowParser();
            var values = new List<object>();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithTooManyValues_ShouldThrow()
        {
            // Arrange
            var parser = new PokemonSheetRowParser();
            var values = new List<object>()
            {
                0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0",
                0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0,
                "0", "", "0", "", "", "", false, false, 0, 0, false, 0.5, 0, 0.5, 0.5, 0,
                "", "", "", "", "", "", "", "excessive value"
            };

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Fact]
        public void ReadRow_WithValuesNull_ShouldThrow()
        {
            // Arrange
            var parser = new PokemonSheetRowParser();
            List<object> values = null;

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }

        [Theory]
        [InlineData("")] // SortIndex must be int
        [InlineData(0, "")] // PokedexNumber must be int
        [InlineData(0, 0, "")] // PokemonSpeciesName must be non-empty
        [InlineData(0, 0, "0", "")] // DefaultVarietyName must be non-empty
        [InlineData(0, 0, "0", "0", "")] // PokemonVarietyName must be non-empty
        [InlineData(0, 0, "0", "0", "0", "")] // ResourceName must be non-empty
        [InlineData(0, 0, "0", "0", "0", "0", "")] // PokemonFormName must be non-empty
        [InlineData(0, 0, "0", "0", "0", "0", "0", "")] // AvailabilityName must be non-empty
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "")] // SpriteName must be non-empty
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", "")] // DoInclude must be boolean
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "")] // DefaultFormName must be non-empty
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", "")] // Attack must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, "")] // SpecialAttack must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, "")] // Defense must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, "")] // SpecialDefense must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, "")] // Speed must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, "")] // HitPoints must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, "")] // AttackEvYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, "")] // SpecialAttackEvYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, "")] // DefenseEvYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, "")] // SpecialDefenseEvYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "")] // SpeedEvYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "")] // HitPointsEvYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "")] // Type1Name must be non-empty
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", 0)] // Type2Name must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "")] // PrimaryAbilityName must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", 0)] // SecondAbilityName must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", 0)] // HiddenAbilityName must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", 0)] // PvpTierName must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", "")] // IsMega must be boolean
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, "")] // IsFullyEvolved must be boolean
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, "")] // Generation must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, "")] // CatchRate must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, "")] // HasGender must be boolean
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, "")] // MaleRatio must be decimal
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, "")] // EggCycles must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, "")] // Height must be decimal
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, "")] // Weight must be decimal
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, "")] // ExpYield must be int
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, 0)] // SmogonUrl must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, "", 0)] // BulbapediaUrl must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, "", "", 0)] // PokeoneCommunityUrl must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, "", "", "", 0)] // PokemonShowdownUrl must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, "", "", "", "", 0)] // SerebiiUrl must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, "", "", "", "", "", 0)] // PokemonDbUrl must be string
        [InlineData(0, 0, "0", "0", "0", "0", "0", "0", "0", false, "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", "", "0", "", "", "", false, false, 0, 0, false, 0.1, 0, 0.1, 0.1, 0, "", "", "", "", "", "", 0)] // Notes must be string
        public void ReadRow_WithUnparsableValue_ShouldThrow(params object[] valuesAsArray)
        {
            // Arrange
            var parser = new PokemonSheetRowParser();
            var values = valuesAsArray.ToList();

            // Act
            var actual = () => parser.ReadRow(values);

            // Assert
            actual.Should().Throw<InvalidRowDataException>();
        }
    }
}
