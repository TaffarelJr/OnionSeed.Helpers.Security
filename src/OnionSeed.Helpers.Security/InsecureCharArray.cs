using System;
using System.Security;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// Provides a tightly controlled context where the unencrypted data in a <see cref="SecureString"/>
	/// can be accessed as safely as possible, as an array of <see cref="char"/>.
	/// </summary>
	/// <remarks>This class is much safer than (and preferred over) <see cref="InsecureString"/>.</remarks>
	public sealed class InsecureCharArray : InsecureBase<char[]>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InsecureCharArray"/> class,
		/// providing access to the unencrypted data in the given <see cref="SecureString"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> to be wrapped.</param>
		/// <exception cref="ArgumentNullException"><paramref name="secureString"/> is <c>null</c>.</exception>
		public InsecureCharArray(SecureString secureString)
			: base(secureString)
		{
		}

		/// <inheritdoc/>
		protected override char[] InitializeBuffer(int chars, int bytes) => new char[chars];
	}
}
