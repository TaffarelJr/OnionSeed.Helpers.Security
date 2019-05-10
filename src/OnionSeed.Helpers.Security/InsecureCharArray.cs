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
	/// can be accessed as safely as possible, as an array of <see cref="char"/>.
	/// </summary>
	/// <remarks>This class is much safer than (and preferred over) <see cref="InsecureString"/>.</remarks>
	[SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Not a true collection class.")]
	public sealed class InsecureCharArray : InsecureBase<char[]>, IEnumerable<char>
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
		public IEnumerator<char> GetEnumerator() => Value == null
			? Enumerable.Empty<char>().GetEnumerator()
			: ((IEnumerable<char>)Value).GetEnumerator();

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		protected override char[] InitializeBuffer(int chars, int bytes) => new char[chars];
	}
}
