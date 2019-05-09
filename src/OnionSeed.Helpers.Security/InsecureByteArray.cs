using System;
using System.Security;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// Provides a tightly controlled context where the unencrypted data in a <see cref="SecureString"/>
	/// can be accessed as safely as possible, as an array of <see cref="byte"/>.
	/// </summary>
	/// <remarks>This class is much safer than (and preferred over) <see cref="InsecureString"/>.</remarks>
	public sealed class InsecureByteArray : InsecureBase<byte[]>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InsecureByteArray"/> class,
		/// providing access to the unencrypted data in the given <see cref="SecureString"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> to be wrapped.</param>
		/// <exception cref="ArgumentNullException"><paramref name="secureString"/> is <c>null</c>.</exception>
		public InsecureByteArray(SecureString secureString)
			: base(secureString)
		{
		}

		/// <inheritdoc/>
		protected override byte[] InitializeBuffer(int chars, int bytes) => new byte[bytes];
	}
}
