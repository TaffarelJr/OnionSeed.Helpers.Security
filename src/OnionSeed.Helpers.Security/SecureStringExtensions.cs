using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;

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

		/// <summary>
		/// Converts the given list of <see cref="char"/> into a <see cref="SecureString"/>.
		/// </summary>
		/// <param name="data">The list of <see cref="char"/> to be secured.</param>
		/// <returns>A new <see cref="SecureString"/> that contains the data from the given list of <see cref="char"/>.</returns>
		public static SecureString Secure(this IEnumerable<char> data)
		{
			if (data == null)
				throw new ArgumentNullException(nameof(data));

			var secure = new SecureString();
			secure.Append(data);
			secure.MakeReadOnly();
			return secure;
		}

		/// <summary>
		/// Converts the data in the given <see cref="StringBuilder"/> into a <see cref="SecureString"/>.
		/// </summary>
		/// <param name="data">The <see cref="StringBuilder"/> that contains the data to be secured.</param>
		/// <returns>A new <see cref="SecureString"/> that contains the data from the given <see cref="StringBuilder"/>.</returns>
		/// <remarks>This overload exists because <see cref="StringBuilder"/> does not implement any of the standard collection interfaces,
		/// and making additional copies of sensitive data (using methods such at <see cref="StringBuilder.ToString()"/>) is discouraged.</remarks>
		public static SecureString Secure(this StringBuilder data)
		{
			if (data == null)
				throw new ArgumentNullException(nameof(data));

			var secure = new SecureString();
			for (int i = 0; i < data.Length; i++)
			{
				secure.AppendChar(data[i]);
			}

			secure.MakeReadOnly();
			return secure;
		}

		/// <summary>
		/// Provides access to the data in the given <see cref="SecureString"/> in a secure, disposable context that works like a <see cref="string"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> whose data is to be accessed.</param>
		/// <returns>A new <see cref="InsecureString"/> that provides access to the data in the given <see cref="SecureString"/>.</returns>
		public static InsecureString ToInsecureString(this SecureString secureString) => new InsecureString(secureString);

		/// <summary>
		/// Provides access to the data in the given <see cref="SecureString"/> in a secure, disposable context that works like a <see cref="string"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> whose data is to be accessed.</param>
		/// <returns>A new <see cref="InsecureCharArray"/> that provides access to the data in the given <see cref="SecureString"/>.</returns>
		public static InsecureCharArray ToInsecureCharArray(this SecureString secureString) => new InsecureCharArray(secureString);

		/// <summary>
		/// Provides access to the data in the given <see cref="SecureString"/> in a secure, disposable context that works like a <see cref="string"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> whose data is to be accessed.</param>
		/// <returns>A new <see cref="InsecureByteArray"/> that provides access to the data in the given <see cref="SecureString"/>.</returns>
		public static InsecureByteArray ToInsecureByteArray(this SecureString secureString) => new InsecureByteArray(secureString);
	}
}
