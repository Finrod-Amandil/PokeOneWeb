using FluentAssertions;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Extensions;
using Xunit;

namespace PokeOneWeb.Data.Tests.Extensions
{
    public class NatureExtensionsTests
    {
        [Fact]
        public void GetDescription_PositiveAttackValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Attack = 1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be("+Atk", "because only the attack value was positive.");
        }

        [Fact]
        public void GetDescription_NegativettackValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Attack = -1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(" / -Atk", "because only the attack value was negative.");
        }

        [Fact]
        public void GetDescription_PositiveSpecialAttackValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                SpecialAttack = 1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be("+SpA", "because only the special attack value was positive.");
        }

        [Fact]
        public void GetDescription_NegativeSpecialAttackValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                SpecialAttack = -1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(" / -SpA", "because only the special attack value was negative.");
        }

        [Fact]
        public void GetDescription_PositiveDefenseValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Defense = 1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be("+Def", "because only the defense value was positive.");
        }

        [Fact]
        public void GetDescription_NegativeDefenseValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Defense = -1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(" / -Def", "because only the defense value was negative.");
        }

        [Fact]
        public void GetDescription_PositiveSpecialDefenseValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                SpecialDefense = 1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be("+SpD", "because only the special defense value was positive.");
        }

        [Fact]
        public void GetDescription_NegativeSpecialDefenseValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                SpecialDefense = -1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(" / -SpD", "because only the special defense value was negative.");
        }

        [Fact]
        public void GetDescription_PositiveSpeedValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Speed = 1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be("+Spe", "because only the speed value was positive.");
        }

        [Fact]
        public void GetDescription_NegativeSpeedValue_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Speed = -1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(" / -Spe", "because only the speed value was negative.");
        }

        [Fact]
        public void GetDescription_AllValuesZero_CorrectString()
        {
            // Given
            Nature nature = new Nature();

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(string.Empty, "because all values were zero.");
        }

        [Fact]
        public void GetDescription_AllValuesPositive_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Attack = 1,
                SpecialAttack = 1,
                Defense = 1,
                SpecialDefense = 1,
                Speed = 1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be("+Spe+SpD+Def+SpA+Atk", "because all values were zero.");
        }

        [Fact]
        public void GetDescription_AllValuesNegative_CorrectString()
        {
            // Given
            Nature nature = new Nature()
            {
                Attack = -1,
                SpecialAttack = -1,
                Defense = -1,
                SpecialDefense = -1,
                Speed = -1
            };

            // When
            string description = nature.GetDescription();

            // Then
            description.Should().Be(" / -Atk / -SpA / -Def / -SpD / -Spe", "because all values were zero.");
        }
    }
}