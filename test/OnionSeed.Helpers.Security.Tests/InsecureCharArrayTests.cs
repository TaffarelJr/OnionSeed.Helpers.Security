﻿using System;
using System.Security;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Security
{
	public class InsecureCharArrayTests
	{
		[Fact]
		public void Constructor_ShouldThrowException_WhenSecureStringIsNull()
		{
			// Arrange
			SecureString secureData = null;

			// Act
			Action action = () => new InsecureCharArray(secureData);

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Constructor_ShouldDecryptSecureString()
		{
			// Arrange
			const string data = "This is some random data";
			using (var secureData = new SecureString())
			{
				secureData.Append(data);
				secureData.MakeReadOnly();

				// Act
				using (var insecureData = new InsecureCharArray(secureData))
				{
					// Assert
					insecureData.Value.Should().Equal(data.ToCharArray());
				}
			}
		}

		[Fact]
		public void Constructor_ShouldDecryptSecureString_WhenUnicodeIsPresent()
		{
			// Arrange
			const string data = "This is some random data, including unicode characters: ѬѾ";
			using (var secureData = new SecureString())
			{
				secureData.Append(data);
				secureData.MakeReadOnly();

				// Act
				using (var insecureData = new InsecureCharArray(secureData))
				{
					// Assert
					insecureData.Value.Should().Equal(data.ToCharArray());
				}
			}
		}
	}
}