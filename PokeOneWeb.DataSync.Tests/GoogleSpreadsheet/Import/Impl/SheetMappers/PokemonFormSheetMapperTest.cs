using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers;
using PokeOneWeb.Shared.Exceptions;
using Xunit;

namespace PokeOneWeb.DataSync.Tests.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class PokemonFormSheetMapperTest
    {
        private readonly Mock<ISpreadsheetImportReporter> _reporterMock;
        private readonly PokemonFormSheetMapper _mapper;

        private readonly RowHash _rowHash = new() { IdHash = "Id Hash", Hash = "Hash", ImportSheetId = 1 };

        private List<string> _columnNames = new()
        {
            "SortIndex",
            "PokedexNumber",
            "PokemonSpecies",
            "DefaultVarietyName",
            "PokemonVariety",
            "PokemonVarietyResourceName",
            "PokemonForm",
            "Availability",
            "SpriteName",
            "DoInclude",
            "DefaultFormName",
            "Atk",
            "Spa",
            "Def",
            "Spd",
            "Spe",
            "Hp",
            "AtkEv",
            "SpaEv",
            "DefEv",
            "SpdEv",
            "SpeEv",
            "HpEv",
            "Type1",
            "Type2",
            "PrimaryAbility",
            "SecondaryAbility",
            "HiddenAbility",
            "PvpTier",
            "IsMega",
            "IsFullyEvolved",
            "Generation",
            "CatchRate",
            "HasGender",
            "MaleRatio",
            "EggCycles",
            "Height",
            "Weight",
            "XpYield",
            "SmogonUrl",
            "BulbapediaUrl",
            "PokeoneCommunityUrl",
            "PokemonShowdownUrl",
            "SerebiiUrl",
            "PokemonDbUrl",
            "Notes"
        };

        private List<object> _values;

        private List<SheetDataRow> Data => new() { new SheetDataRow(_columnNames, _rowHash, _values) };

        public PokemonFormSheetMapperTest()
        {
            _reporterMock = new Mock<ISpreadsheetImportReporter>();
            _mapper = new PokemonFormSheetMapper(_reporterMock.Object);
        }

        [Fact]
        public void Map_WithMinimalValidValues_ShouldMap()
        {
            // Arrange
            const int sortIndex = 123;
            const int pokedexNumber = 111;
            const string pokemonSpecies = "Pokemon Species";
            const string defaultVariety = "Default Variety Name";
            const string pokemonVariety = "Pokemon Variety";
            const string resourceName = "Resource Name";
            const string pokemonForm = "Pokemon Form";
            const string availability = "Availability";
            const string spriteName = "sprite.png";
            const bool doInclude = true;
            const string defaultForm = "Default Form";
            const int atk = 10;
            const int spa = 20;
            const int def = 30;
            const int spd = 40;
            const int spe = 50;
            const int hp = 60;
            const int atkEv = 1;
            const int spaEv = 2;
            const int defEv = 3;
            const int spdEv = 1;
            const int speEv = 2;
            const int hpEv = 3;
            const string primaryType = "Primary Type";
            const string secondaryType = "";
            const string primaryAbility = "Primary Ability";
            const string secondaryAbility = "";
            const string hiddenAbility = "";
            const string pvpTier = "PVP Tier";
            const bool isMega = true;
            const bool isFullyEvolved = false;
            const int generation = 4;
            const int catchRate = 45;
            const bool hasGender = false;
            const decimal maleRatio = 87.5M;
            const int eggCycles = 20;
            const decimal height = 123.4M;
            const decimal weight = 12.3M;
            const int xpYield = 142;

            _values = new List<object>
            {
                sortIndex,
                pokedexNumber,
                pokemonSpecies,
                defaultVariety,
                pokemonVariety,
                resourceName,
                pokemonForm,
                availability,
                spriteName,
                doInclude,
                defaultForm,
                atk,
                spa,
                def,
                spd,
                spe,
                hp,
                atkEv,
                spaEv,
                defEv,
                spdEv,
                speEv,
                hpEv,
                primaryType,
                secondaryType,
                primaryAbility,
                secondaryAbility,
                hiddenAbility,
                pvpTier,
                isMega,
                isFullyEvolved,
                generation,
                catchRate,
                hasGender,
                maleRatio,
                eggCycles,
                height,
                weight,
                xpYield
            };

            var expected = new PokemonForm
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                SortIndex = sortIndex,
                Name = pokemonForm,
                AvailabilityName = availability,
                SpriteName = spriteName,
                PokemonVariety = new PokemonVariety
                {
                    Name = pokemonVariety,
                    ResourceName = resourceName,
                    DoInclude = doInclude,
                    DefaultFormName = defaultForm,
                    PokemonSpecies = new PokemonSpecies
                    {
                        PokedexNumber = pokedexNumber,
                        Name = pokemonSpecies,
                        DefaultVarietyName = defaultVariety
                    },
                    Attack = atk,
                    SpecialAttack = spa,
                    Defense = def,
                    SpecialDefense = spd,
                    Speed = spe,
                    HitPoints = hp,
                    AttackEv = atkEv,
                    SpecialAttackEv = spaEv,
                    DefenseEv = defEv,
                    SpecialDefenseEv = spdEv,
                    SpeedEv = speEv,
                    HitPointsEv = hpEv,
                    PrimaryTypeName = primaryType,
                    SecondaryTypeName = secondaryType,
                    PrimaryAbilityName = primaryAbility,
                    SecondaryAbilityName = secondaryAbility,
                    HiddenAbilityName = hiddenAbility,
                    PvpTierName = pvpTier,
                    IsMega = isMega,
                    IsFullyEvolved = isFullyEvolved,
                    Generation = generation,
                    CatchRate = catchRate,
                    HasGender = hasGender,
                    MaleRatio = maleRatio,
                    EggCycles = eggCycles,
                    Height = height,
                    Weight = weight,
                    ExpYield = xpYield,
                    Notes = string.Empty,
                    Urls = new List<PokemonVarietyUrl>()
                }
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithAllValidValues_ShouldMap()
        {
            // Arrange
            const int sortIndex = 123;
            const int pokedexNumber = 111;
            const string pokemonSpecies = "Pokemon Species";
            const string defaultVariety = "Default Variety Name";
            const string pokemonVariety = "Pokemon Variety";
            const string resourceName = "Resource Name";
            const string pokemonForm = "Pokemon Form";
            const string availability = "Availability";
            const string spriteName = "sprite.png";
            const bool doInclude = true;
            const string defaultForm = "Default Form";
            const int atk = 10;
            const int spa = 20;
            const int def = 30;
            const int spd = 40;
            const int spe = 50;
            const int hp = 60;
            const int atkEv = 1;
            const int spaEv = 2;
            const int defEv = 3;
            const int spdEv = 1;
            const int speEv = 2;
            const int hpEv = 3;
            const string primaryType = "Primary Type";
            const string secondaryType = "Secondary Type";
            const string primaryAbility = "Primary Ability";
            const string secondaryAbility = "Secondary Ability";
            const string hiddenAbility = "Hidden Ability";
            const string pvpTier = "PVP Tier";
            const bool isMega = true;
            const bool isFullyEvolved = false;
            const int generation = 4;
            const int catchRate = 45;
            const bool hasGender = false;
            const decimal maleRatio = 87.5M;
            const int eggCycles = 20;
            const decimal height = 123.4M;
            const decimal weight = 12.3M;
            const int xpYield = 142;
            const string smogonUrl = "smogon/url";
            const string bulbapediaUrl = "bulbapedia/url";
            const string pokeoneCommunityUrl = "pokeonecommunity/url";
            const string pokemonShowdownUrl = "pokemonshowdown/url";
            const string serebiiUrl = "serebii/url";
            const string pokemonDbUrl = "pokemondb/url";
            const string notes = "Notes";

            _values = new List<object>
            {
                sortIndex,
                pokedexNumber,
                pokemonSpecies,
                defaultVariety,
                pokemonVariety,
                resourceName,
                pokemonForm,
                availability,
                spriteName,
                doInclude,
                defaultForm,
                atk,
                spa,
                def,
                spd,
                spe,
                hp,
                atkEv,
                spaEv,
                defEv,
                spdEv,
                speEv,
                hpEv,
                primaryType,
                secondaryType,
                primaryAbility,
                secondaryAbility,
                hiddenAbility,
                pvpTier,
                isMega,
                isFullyEvolved,
                generation,
                catchRate,
                hasGender,
                maleRatio,
                eggCycles,
                height,
                weight,
                xpYield,
                smogonUrl,
                bulbapediaUrl,
                pokeoneCommunityUrl,
                pokemonShowdownUrl,
                serebiiUrl,
                pokemonDbUrl,
                notes
            };

            var expected = new PokemonForm
            {
                IdHash = _rowHash.IdHash,
                Hash = _rowHash.Hash,
                ImportSheetId = _rowHash.ImportSheetId,
                SortIndex = sortIndex,
                Name = pokemonForm,
                AvailabilityName = availability,
                SpriteName = spriteName,
                PokemonVariety = new PokemonVariety
                {
                    Name = pokemonVariety,
                    ResourceName = resourceName,
                    DoInclude = doInclude,
                    DefaultFormName = defaultForm,
                    PokemonSpecies = new PokemonSpecies
                    {
                        PokedexNumber = pokedexNumber,
                        Name = pokemonSpecies,
                        DefaultVarietyName = defaultVariety
                    },
                    Attack = atk,
                    SpecialAttack = spa,
                    Defense = def,
                    SpecialDefense = spd,
                    Speed = spe,
                    HitPoints = hp,
                    AttackEv = atkEv,
                    SpecialAttackEv = spaEv,
                    DefenseEv = defEv,
                    SpecialDefenseEv = spdEv,
                    SpeedEv = speEv,
                    HitPointsEv = hpEv,
                    PrimaryTypeName = primaryType,
                    SecondaryTypeName = secondaryType,
                    PrimaryAbilityName = primaryAbility,
                    SecondaryAbilityName = secondaryAbility,
                    HiddenAbilityName = hiddenAbility,
                    PvpTierName = pvpTier,
                    IsMega = isMega,
                    IsFullyEvolved = isFullyEvolved,
                    Generation = generation,
                    CatchRate = catchRate,
                    HasGender = hasGender,
                    MaleRatio = maleRatio,
                    EggCycles = eggCycles,
                    Height = height,
                    Weight = weight,
                    ExpYield = xpYield,
                    Notes = notes,
                    Urls = new List<PokemonVarietyUrl>
                    {
                        new() { Name = "Smogon", Url = smogonUrl },
                        new() { Name = "Bulbapedia", Url = bulbapediaUrl },
                        new() { Name = "PokeoneCommunity", Url = pokeoneCommunityUrl },
                        new() { Name = "Pokemon Showdown", Url = pokemonShowdownUrl },
                        new() { Name = "Serebii", Url = serebiiUrl },
                        new() { Name = "PokemonDB", Url = pokemonDbUrl },
                    }
                }
            };

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(1);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithMultipleRows_ShouldMap()
        {
            // Arrange
            const int sortIndex1 = 123;
            const int pokedexNumber1 = 111;
            const string pokemonSpecies1 = "Pokemon Species 1";
            const string defaultVariety1 = "Default Variety Name 1";
            const string pokemonVariety1 = "Pokemon Variety 1";
            const string resourceName1 = "Resource Name 1";
            const string pokemonForm1 = "Pokemon Form 1";
            const string availability1 = "Availability 1";
            const string spriteName1 = "sprite1.png";
            const bool doInclude1 = true;
            const string defaultForm1 = "Default Form 1";
            const int atk1 = 10;
            const int spa1 = 20;
            const int def1 = 30;
            const int spd1 = 40;
            const int spe1 = 50;
            const int hp1 = 60;
            const int atkEv1 = 1;
            const int spaEv1 = 2;
            const int defEv1 = 3;
            const int spdEv1 = 1;
            const int speEv1 = 2;
            const int hpEv1 = 3;
            const string primaryType1 = "Primary Type 1";
            const string secondaryType1 = "";
            const string primaryAbility1 = "Primary Ability 1";
            const string secondaryAbility1 = "";
            const string hiddenAbility1 = "";
            const string pvpTier1 = "PVP Tier 1";
            const bool isMega1 = true;
            const bool isFullyEvolved1 = false;
            const int generation1 = 4;
            const int catchRate1 = 45;
            const bool hasGender1 = false;
            const decimal maleRatio1 = 87.5M;
            const int eggCycles1 = 20;
            const decimal height1 = 123.4M;
            const decimal weight1 = 12.3M;
            const int xpYield1 = 142;

            const int sortIndex2 = 234;
            const int pokedexNumber2 = 222;
            const string pokemonSpecies2 = "Pokemon Species 2";
            const string defaultVariety2 = "Default Variety Name 2";
            const string pokemonVariety2 = "Pokemon Variety 2";
            const string resourceName2 = "Resource Name 2";
            const string pokemonForm2 = "Pokemon Form 2";
            const string availability2 = "Availability 2";
            const string spriteName2 = "sprite2.png";
            const bool doInclude2 = false;
            const string defaultForm2 = "Default Form 2";
            const int atk2 = 12;
            const int spa2 = 22;
            const int def2 = 32;
            const int spd2 = 42;
            const int spe2 = 52;
            const int hp2 = 62;
            const int atkEv2 = 2;
            const int spaEv2 = 3;
            const int defEv2 = 1;
            const int spdEv2 = 2;
            const int speEv2 = 3;
            const int hpEv2 = 1;
            const string primaryType2 = "Primary Type 2";
            const string secondaryType2 = "Secondary Type 2";
            const string primaryAbility2 = "Primary Ability 2";
            const string secondaryAbility2 = "Secondary Ability 2";
            const string hiddenAbility2 = "Hidden Ability 2";
            const string pvpTier2 = "PVP Tier 2";
            const bool isMega2 = false;
            const bool isFullyEvolved2 = true;
            const int generation2 = 7;
            const int catchRate2 = 129;
            const bool hasGender2 = true;
            const decimal maleRatio2 = 100;
            const int eggCycles2 = 10;
            const decimal height2 = 234.5M;
            const decimal weight2 = 23.4M;
            const int xpYield2 = 355;

            var values1 = new List<object>
            {
                sortIndex1,
                pokedexNumber1,
                pokemonSpecies1,
                defaultVariety1,
                pokemonVariety1,
                resourceName1,
                pokemonForm1,
                availability1,
                spriteName1,
                doInclude1,
                defaultForm1,
                atk1,
                spa1,
                def1,
                spd1,
                spe1,
                hp1,
                atkEv1,
                spaEv1,
                defEv1,
                spdEv1,
                speEv1,
                hpEv1,
                primaryType1,
                secondaryType1,
                primaryAbility1,
                secondaryAbility1,
                hiddenAbility1,
                pvpTier1,
                isMega1,
                isFullyEvolved1,
                generation1,
                catchRate1,
                hasGender1,
                maleRatio1,
                eggCycles1,
                height1,
                weight1,
                xpYield1
            };

            var values2 = new List<object>
            {
                sortIndex2,
                pokedexNumber2,
                pokemonSpecies2,
                defaultVariety2,
                pokemonVariety2,
                resourceName2,
                pokemonForm2,
                availability2,
                spriteName2,
                doInclude2,
                defaultForm2,
                atk2,
                spa2,
                def2,
                spd2,
                spe2,
                hp2,
                atkEv2,
                spaEv2,
                defEv2,
                spdEv2,
                speEv2,
                hpEv2,
                primaryType2,
                secondaryType2,
                primaryAbility2,
                secondaryAbility2,
                hiddenAbility2,
                pvpTier2,
                isMega2,
                isFullyEvolved2,
                generation2,
                catchRate2,
                hasGender2,
                maleRatio2,
                eggCycles2,
                height2,
                weight2,
                xpYield2
            };

            var data = new List<SheetDataRow>
            {
                new(_columnNames, _rowHash, values1),
                new(_columnNames, _rowHash, values2)
            };

            var expected = new List<PokemonForm>
            {
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    SortIndex = sortIndex1,
                    Name = pokemonForm1,
                    AvailabilityName = availability1,
                    SpriteName = spriteName1,
                    PokemonVariety = new PokemonVariety
                    {
                        Name = pokemonVariety1,
                        ResourceName = resourceName1,
                        DoInclude = doInclude1,
                        DefaultFormName = defaultForm1,
                        PokemonSpecies = new PokemonSpecies
                        {
                            PokedexNumber = pokedexNumber1,
                            Name = pokemonSpecies1,
                            DefaultVarietyName = defaultVariety1
                        },
                        Attack = atk1,
                        SpecialAttack = spa1,
                        Defense = def1,
                        SpecialDefense = spd1,
                        Speed = spe1,
                        HitPoints = hp1,
                        AttackEv = atkEv1,
                        SpecialAttackEv = spaEv1,
                        DefenseEv = defEv1,
                        SpecialDefenseEv = spdEv1,
                        SpeedEv = speEv1,
                        HitPointsEv = hpEv1,
                        PrimaryTypeName = primaryType1,
                        SecondaryTypeName = secondaryType1,
                        PrimaryAbilityName = primaryAbility1,
                        SecondaryAbilityName = secondaryAbility1,
                        HiddenAbilityName = hiddenAbility1,
                        PvpTierName = pvpTier1,
                        IsMega = isMega1,
                        IsFullyEvolved = isFullyEvolved1,
                        Generation = generation1,
                        CatchRate = catchRate1,
                        HasGender = hasGender1,
                        MaleRatio = maleRatio1,
                        EggCycles = eggCycles1,
                        Height = height1,
                        Weight = weight1,
                        ExpYield = xpYield1,
                        Notes = string.Empty,
                        Urls = new List<PokemonVarietyUrl>()
                    }
                },
                new()
                {
                    IdHash = _rowHash.IdHash,
                    Hash = _rowHash.Hash,
                    ImportSheetId = _rowHash.ImportSheetId,
                    SortIndex = sortIndex2,
                    Name = pokemonForm2,
                    AvailabilityName = availability2,
                    SpriteName = spriteName2,
                    PokemonVariety = new PokemonVariety
                    {
                        Name = pokemonVariety2,
                        ResourceName = resourceName2,
                        DoInclude = doInclude2,
                        DefaultFormName = defaultForm2,
                        PokemonSpecies = new PokemonSpecies
                        {
                            PokedexNumber = pokedexNumber2,
                            Name = pokemonSpecies2,
                            DefaultVarietyName = defaultVariety2
                        },
                        Attack = atk2,
                        SpecialAttack = spa2,
                        Defense = def2,
                        SpecialDefense = spd2,
                        Speed = spe2,
                        HitPoints = hp2,
                        AttackEv = atkEv2,
                        SpecialAttackEv = spaEv2,
                        DefenseEv = defEv2,
                        SpecialDefenseEv = spdEv2,
                        SpeedEv = speEv2,
                        HitPointsEv = hpEv2,
                        PrimaryTypeName = primaryType2,
                        SecondaryTypeName = secondaryType2,
                        PrimaryAbilityName = primaryAbility2,
                        SecondaryAbilityName = secondaryAbility2,
                        HiddenAbilityName = hiddenAbility2,
                        PvpTierName = pvpTier2,
                        IsMega = isMega2,
                        IsFullyEvolved = isFullyEvolved2,
                        Generation = generation2,
                        CatchRate = catchRate2,
                        HasGender = hasGender2,
                        MaleRatio = maleRatio2,
                        EggCycles = eggCycles2,
                        Height = height2,
                        Weight = weight2,
                        ExpYield = xpYield2,
                        Notes = string.Empty,
                        Urls = new List<PokemonVarietyUrl>()
                    }
                }
            };

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(2);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Map_WithMissingRequiredValues_ShouldNotParseAndReportErrors()
        {
            // Arrange
            _values = new List<object>();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(PokemonForm), _rowHash.IdHash, It.IsAny<Exception>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Map_WithNull_ShouldThrow()
        {
            // Arrange
            List<SheetDataRow> data = null;

            // Act
            var actual = () => _mapper.Map(data).ToList();

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Map_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var data = new List<SheetDataRow>();

            // Act
            var actual = _mapper.Map(data).ToList();

            // Assert
            actual.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("notInt", 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, "notInt", "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", "notBool", "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", "notInt", 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, "notInt", 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, "notInt", 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, "notInt", 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, "notInt", 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, "notInt", 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, "notInt", 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, "notInt", 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, "notInt", 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, "notInt", 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, "notInt", 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, "notInt", "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", 1000, "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", 1000, "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", 1000, "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", "notBool", false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, "notBool", 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, "notInt", 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, "notInt", false, 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, "notBool", 1, 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, "notInt", 1, 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, "notInt", 1, 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, "notInt", 1, 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, "notInt", 1, "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, "notInt", "", "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, 1000, "", "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", 1000, "", "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", 1000, "", "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", 1000, "", "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", 1000, "", "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", 1000, "")]
        [InlineData(1, 1, "ps", "dvn", "pv", "pvrn", "pf", "a", "s", false, "dfn", 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, "t1", "", "a1", "", "", "p", false, false, 1, 1, false, 1, 1, 1, 1, 1, "", "", "", "", "", "", 1000)]
        public void Map_WithUnparsableValue_ShouldReportError(params object[] valuesAsArray)
        {
            // Arrange
            _values = valuesAsArray.ToList();

            // Act
            var actual = _mapper.Map(Data).ToList();

            // Assert
            actual.Count.Should().Be(0);
            _reporterMock.Verify(
                x => x.ReportError(nameof(PokemonForm), _rowHash.IdHash, It.IsAny<ParseException>()),
                Times.AtLeastOnce);
        }
    }
}