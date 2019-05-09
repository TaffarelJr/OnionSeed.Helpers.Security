using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// Contains extension methods relating to the <see cref="SecureString"/> data type.
	/// </summary>
	public static class SecureStringExtensions
	{
		/// <summary>
		/// Appends all the specified characters to the end of the given <see cref="SecureString"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> to which the characters should be appended.</param>
		/// <param name="chars">The list of characters to append to the given <see cref="SecureString"/>.</param>
		/// <exception cref="ObjectDisposedException">This <see cref="SecureString"/> has already been disposed.</exception>
		/// <exception cref="InvalidOperationException">This <see cref="SecureString"/> is read-only.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Performing this operation would make the length of this <see cref="SecureString"/> greater than 65,536 characters.</exception>
		/// <exception cref="CryptographicException">An error occurred while protecting or unprotecting the value of this <see cref="SecureString"/>.</exception>
		public static void Append(this SecureString secureString, IEnumerable<char> chars)
		{
			if (secureString == null)
				throw new ArgumentNullException(nameof(secureString));

			if (chars != null)
			{
				foreach (var c in chars)
				{
					secureString.AppendChar(c);
				}
			}
		}
	}
}
