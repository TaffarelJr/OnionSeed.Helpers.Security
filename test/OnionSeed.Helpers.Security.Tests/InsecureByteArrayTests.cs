using System;
using System.Collections;
using System.Security;
using System.Text;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Security
{
	public class InsecureByteArrayTests
	{
		[Fact]
		public void Constructor_ShouldThrowException_WhenSecureStringIsNull()
		{
			// Arrange
			SecureString secureData = null;

			// Act
			Action action = () => new InsecureByteArray(secureData);

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
				using (var subject = new InsecureByteArray(secureData))
				{
					// Assert
					subject.Value.Should().Equal(Encoding.Unicode.GetBytes(data));
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
				using (var subject = new InsecureByteArray(secureData))
				{
					// Assert
					subject.Value.Should().Equal(Encoding.Unicode.GetBytes(data));
				}
			}
		}

		[Fact]
		public void GetEnumerator_ShouldNotEnumerate_WhenSecureStringHasNoData()
		{
			// Arrange
			using (var secureData = new SecureString())
			{
				secureData.MakeReadOnly();
				using (var subject = new InsecureByteArray(secureData))
				{
					// Act
					foreach (var b in subject)
					{
						// Assert
						Assert.True(false, "Should not enumerate.");
					}
				}
			}
		}

		[Fact]
		public void GetEnumerator_ShouldEnumerate()
		{
			// Arrange
			const string data = "Data with - oh no - UNICODE!!  이 테스트는 대단합니다";
			var bytes = Encoding.Unicode.GetBytes(data);
			var i = 0;

			using (var secureData = new SecureString())
			{
				secureData.Append(data);
				secureData.MakeReadOnly();
				using (var subject = new InsecureByteArray(secureData))
				{
					// Act
					foreach (var b in subject)
					{
						// Assert
						b.Should().Be(bytes[i]);
						i++;
					}

					i.Should().Be(bytes.Length);
				}
			}
		}

		[Fact]
		public void GetEnumerator_ShouldNotEnumerate_WhenSecureStringHasNoData_AndTypeIsNotSpecified()
		{
			// Arrange
			using (var secureData = new SecureString())
			{
				secureData.MakeReadOnly();
				using (var subject = new InsecureByteArray(secureData))
				{
					// Act
					foreach (var b in (IEnumerable)subject)
					{
						// Assert
						Assert.True(false, "Should not enumerate.");
					}
				}
			}
		}

		[Fact]
		public void GetEnumerator_ShouldEnumerate_WhenTypeIsNotSpecified()
		{
			// Arrange
			const string data = "Data with - oh no - UNICODE!!  이 테스트는 대단합니다";
			var bytes = Encoding.Unicode.GetBytes(data);
			var i = 0;

			using (var secureData = new SecureString())
			{
				secureData.Append(data);
				secureData.MakeReadOnly();
				using (var subject = new InsecureByteArray(secureData))
				{
					// Act
					foreach (var o in (IEnumerable)subject)
					{
						// Assert
						o.Should().BeOfType<byte>();

						var b = (byte)o;
						b.Should().Be(bytes[i]);
						i++;
					}

					i.Should().Be(bytes.Length);
				}
			}
		}
	}
}
