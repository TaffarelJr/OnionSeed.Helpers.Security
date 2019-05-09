using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// Provides a tightly controlled context where the unencrypted data in a <see cref="SecureString"/>
	/// can be accessed as safely as possible, as an array of <see cref="byte"/>.
	/// </summary>
	/// <remarks>This class is much safer than (and preferred over) <see cref="InsecureString"/>.</remarks>
	[SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Not a true collection class.")]
	public sealed class InsecureByteArray : InsecureBase<byte[]>, IEnumerable<byte>
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
		public IEnumerator<byte> GetEnumerator() => Value == null
			? Enumerable.Empty<byte>().GetEnumerator()
			: ((IEnumerable<byte>)Value).GetEnumerator();

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		protected override byte[] InitializeBuffer(int chars, int bytes) => new byte[bytes];
	}
}
