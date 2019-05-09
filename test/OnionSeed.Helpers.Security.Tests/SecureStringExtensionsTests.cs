using System;
using System.Linq;
using System.Security;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Security
{
	public class SecureStringExtensionsTests
	{
		[Fact]
		public void Append_ShouldThrowException_WhenSecureStringIsNull()
		{
			// Arrange
			SecureString secureData = null;

			// Act
			Action action = () => secureData.Append("bob");

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Append_ShouldDoNothing_WhenListIsNull()
		{
			// Arrange
			using (var secureData = new SecureString())
			{
				// Act
				secureData.Append(null);

				// Assert
				secureData.Length.Should().Be(0);
			}
		}

		[Fact]
		public void Append_ShouldDoNothing_WhenListIsEmpty()
		{
			// Arrange
			using (var secureData = new SecureString())
			{
				// Act
				secureData.Append(Enumerable.Empty<char>());

				// Assert
				secureData.Length.Should().Be(0);
			}
		}

		[Fact]
		public void Append_ShouldAppendData()
		{
			// Arrange
			const string data = "Bob's unicode data: №Ω";
			using (var secureData = new SecureString())
			{
				// Act
				secureData.Append(data);

				// Assert
				secureData.Length.Should().Be(data.Length);
			}
		}
	}
}
