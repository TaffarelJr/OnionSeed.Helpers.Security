using System;
using System.Runtime.CompilerServices;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// Contains extension methods for the <see cref="Action"/> data types.
	/// </summary>
	public static class ActionExtensions
	{
		/// <summary>
		/// Executes the given <see cref="Action"/> in a <a href="https://docs.microsoft.com/en-us/dotnet/framework/performance/constrained-execution-regions">Constrained Execution Region</a> in memory.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> to be executed.</param>
		/// <exception cref="ArgumentNullException"><paramref name="action"/> is <c>null</c>.</exception>
		public static void ExecuteInConstrainedRegion(this Action action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				action();
			}
		}
	}
}
