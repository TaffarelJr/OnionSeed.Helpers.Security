using System;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Security
{
	public class ActionExtensionsTests
	{
		[Fact]
		public void ExecuteInConstrainedRegion_ShouldThrowException_WhenActionIsNull()
		{
			// Arrange
			Action subject = null;

			// Act
			Action action = () => subject.ExecuteInConstrainedRegion();

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void ExecuteInConstrainedRegion_ShouldInvokeAction_WhenActionIsGiven()
		{
			// Arrange
			var result = false;
			Action action = () => result = true;

			// Act
			action.ExecuteInConstrainedRegion();

			// Assert
			result.Should().BeTrue();
		}
	}
}
