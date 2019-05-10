using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
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
			SecureString subject = null;

			// Act
			Action action = () => subject.Append("bob");

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Append_ShouldDoNothing_WhenListIsNull()
		{
			// Arrange
			using (var subject = new SecureString())
			{
				// Act
				subject.Append(null);

				// Assert
				subject.Length.Should().Be(0);
			}
		}

		[Fact]
		public void Append_ShouldDoNothing_WhenListIsEmpty()
		{
			// Arrange
			using (var subject = new SecureString())
			{
				// Act
				subject.Append(Enumerable.Empty<char>());

				// Assert
				subject.Length.Should().Be(0);
			}
		}

		[Fact]
		public void Append_ShouldAppendData()
		{
			// Arrange
			const string data = "Bob's unicode data: №Ω";
			using (var subject = new SecureString())
			{
				// Act
				subject.Append(data);

				// Assert
				subject.Length.Should().Be(data.Length);
			}
		}

		[Fact]
		public void Secure_ShouldThrowException_WhenStringValueIsNull()
		{
			// Arrange
			string subject = null;

			// Act
			Action action = () => subject.Secure();

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Secure_ShouldThrowException_WhenEnumerableIsNull()
		{
			// Arrange
			IEnumerable<char> subject = null;

			// Act
			Action action = () => subject.Secure();

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Secure_ShouldThrowException_WhenStringBuilderIsNull()
		{
			// Arrange
			StringBuilder subject = null;

			// Act
			Action action = () => subject.Secure();

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Secure_ShouldSecureData_WhenDataIsString()
		{
			// Arrange
			var subject = "Bob's your uncle";

			// Act
			using (var result = subject.Secure())
			{
				// Assert
				result.IsReadOnly().Should().BeTrue();
				result.Length.Should().Be(16);
			}
		}

		[Fact]
		public void Secure_ShouldSecureData_WhenDataIsEnumerable()
		{
			// Arrange
			var subject = "Bob's your uncle".ToCharArray();

			// Act
			using (var result = subject.Secure())
			{
				// Assert
				result.IsReadOnly().Should().BeTrue();
				result.Length.Should().Be(16);
			}
		}

		[Fact]
		public void Secure_ShouldSecureData_WhenDataIsStringBuilder()
		{
			// Arrange
			var subject = new StringBuilder("Bob's your uncle");

			// Act
			using (var result = subject.Secure())
			{
				// Assert
				result.IsReadOnly().Should().BeTrue();
				result.Length.Should().Be(16);
			}
		}

		[Fact]
		public void ToInsecureString_ShouldReturnNewInsecureString()
		{
			// Arrange
			const string data = "This is a test!";
			var subject = data.Secure();

			// Act
			using (var result = subject.ToInsecureString())
			{
				// Assert
				result.Value.Should().Be(data);
			}
		}

		[Fact]
		public void ToInsecureCharArray_ShouldReturnNewInsecureCharArray()
		{
			// Arrange
			const string data = "This is a test!";
			var subject = data.Secure();

			// Act
			using (var result = subject.ToInsecureCharArray())
			{
				// Assert
				result.Value.Should().Equal(data.ToCharArray());
			}
		}

		[Fact]
		public void ToInsecureByteArray_ShouldReturnNewInsecureByteArray()
		{
			// Arrange
			const string data = "This is a test!";
			var subject = data.Secure();

			// Act
			using (var result = subject.ToInsecureByteArray())
			{
				// Assert
				result.Value.Should().Equal(Encoding.Unicode.GetBytes(data));
			}
		}
	}
}
