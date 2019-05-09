﻿using System;
using System.Security;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// Provides a tightly controlled context where the unencrypted data in a <see cref="SecureString"/>
	/// can be accessed as safely as possible, as a <see cref="string"/>.
	/// </summary>
	/// <remarks>This class is the least secure way to access the encrypted data in a <see cref="SecureString"/>,
	/// but may be necessary in some cases. Where possible, use <see cref="InsecureCharArray"/> or <see cref="InsecureByteArray"/> instead.</remarks>
	public sealed class InsecureString : InsecureBase<string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InsecureString"/> class,
		/// providing access to the unencrypted data in the given <see cref="SecureString"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> to be wrapped.</param>
		/// <exception cref="ArgumentNullException"><paramref name="secureString"/> is <c>null</c>.</exception>
		public InsecureString(SecureString secureString)
			: base(secureString)
		{
		}

		/// <inheritdoc/>
		protected override string InitializeBuffer(int chars, int bytes) => new string('\0', chars);
	}
}
