using FluentAssertions;
using PokeOneWeb.Shared.Extensions;
using Xunit;

namespace PokeOneWeb.Shared.Tests.Extensions
{
    public class StringExtensionsTests
    {
        private const string TEST_OBJECT = "Test";

        [Fact]
        public void EqualExact_SameObject_IsEqual()
        {
            // When
            bool output = TEST_OBJECT.EqualsExact(TEST_OBJECT);

            // Then
            output.Should().BeTrue("because the input is the same object.");
        }

        [Fact]
        public void EqualExact_SameValue_IsEqual()
        {
            // When
            bool output = TEST_OBJECT.EqualsExact("Test");

            // Then
            output.Should().BeTrue("because the input is the same value.");
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("abc")]
        public void EqualExact_UnequalInput_IsNotEqual(string compare)
        {
            // When
            bool output = TEST_OBJECT.EqualsExact(compare);

            // Then
            output.Should().BeFalse("because the input is the not the same value.");
        }
    }
}