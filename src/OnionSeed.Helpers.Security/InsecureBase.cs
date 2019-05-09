using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace OnionSeed.Helpers.Security
{
	/// <summary>
	/// The base implementation of a tightly controlled context where the unencrypted data in a <see cref="SecureString"/>
	/// can be accessed as safely as possible.
	/// </summary>
	/// <typeparam name="T">The data type to be used to access the unencrypted data in the <see cref="SecureString"/>.</typeparam>
	/// <remarks>This code is adapted from sample code at the <a href="http://netvignettes.wordpress.com/2011/05/09/how-to-use-securestring/">.NET Vignettes</a> blog.</remarks>
	public abstract class InsecureBase<T> : IDisposable
	{
		private readonly SecureString _secureString;
		private GCHandle _gcHandle;
		private int _byteCount;
		private bool disposedValue = false;  // To detect redundant calls

		/// <summary>
		/// Initializes a new instance of the <see cref="InsecureBase{T}"/> class,
		/// providing access to the unencrypted data in the given <see cref="SecureString"/>.
		/// </summary>
		/// <param name="secureString">The <see cref="SecureString"/> to be wrapped.</param>
		/// <exception cref="ArgumentNullException"><paramref name="secureString"/> is <c>null</c>.</exception>
		protected InsecureBase(SecureString secureString)
		{
			_secureString = secureString ?? throw new ArgumentNullException(nameof(secureString));
			Initialize();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="InsecureBase{T}"/> class.
		/// </summary>
		/// <remarks>This destructor will run only if the <see cref="IDisposable.Dispose"/> method does not get called.
		/// It gives the base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.</remarks>
		~InsecureBase()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing).
			Dispose(false);
		}

		/// <summary>
		/// Gets the unencrypted data from the <see cref="SecureString"/> being wrapped.
		/// </summary>
		public T Value { get; private set; }

		/// <inheritdoc/>
		/// <remarks>This code added to correctly implement the disposable pattern.</remarks>
#if !DEBUG
		[DebuggerHidden]
#endif
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing).
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> if the method has been called directly or indirectly by a user's code.
		/// Managed and unmanaged resources can be disposed in this mode.
		/// <c>false</c> if the method has been called by the runtime from inside the finalizer and you should not
		/// reference other objects. Only unmanaged resources can be disposed in this mode.</param>
#if !DEBUG
		[DebuggerHidden]
#endif
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects)
				unsafe
				{
					if (_gcHandle.IsAllocated)
					{
						// Get the address of our gcHandle and set all chars to 0's
						var insecurePointer = (char*)_gcHandle.AddrOfPinnedObject();
						for (var i = 0; i < _secureString.Length; i++)
						{
							insecurePointer[i] = '\0';
						}
	#if DEBUG
						var disposed = "¡DISPOSED¡";
						disposed = disposed.Substring(0, Math.Min(disposed.Length, _secureString.Length));
						for (var i = 0; i < disposed.Length; ++i)
						{
							insecurePointer[i] = disposed[i];
						}
	#endif
						_gcHandle.Free();
					}
				}

				// Note disposing has been done
				disposedValue = true;
			}
		}

		/// <summary>
		/// Returns a new buffer of type <typeparamref name="T"/>, filled with 0's.
		/// </summary>
		/// <param name="chars">The <see cref="char"/> count required for the initial value.</param>
		/// <param name="bytes">The <see cref="byte"/> count required for the initial value.</param>
		/// <returns>A new value of type <typeparamref name="T"/>, filled with the specified number of 0's,
		/// to act as a buffer for the unencrypted data.</returns>
		/// <remarks>The number of required 0's is provided in both <see cref="char"/> and <see cref="byte"/> counts
		/// because they might differ depending on whether there are unicode characters included in the data.
		/// Subclasses can use either one to provide an adequate buffer.</remarks>
		protected abstract T InitializeBuffer(int chars, int bytes);

		/// <summary>
		/// Creates an unmanaged context where the unencrypted data in the current <see cref="SecureString"/> can be accessed.
		/// </summary>
#if !DEBUG
		[DebuggerHidden]
#endif
		private void Initialize()
		{
			unsafe
			{
				// We're about to create an unencrypted version of our sensitive string and store it in memory.
				// Don't let anyone (GC) make a copy.
				// To do this, create a new gc handle so we can "pin" the memory.
				// The gc handle will be pinned, and later we will put info in this string.
				_gcHandle = default;

				// insecurePointer will be temporarily used to access the SecureString
				var insecurePointer = IntPtr.Zero;

				// This delegate defines the code that will be executed (similar to the code in a Try block)
				void TryCode(object userData)
				{
					// Even though this code executes in the ExecuteCodeWithGuaranteedCleanup() method, processing can be interupted.
					// We need to make sure nothing happens between when memory is allocated and
					// when the pointers have been assigned values. Otherwise, we can't cleanup later.
					// ConstrainedExecutionRegions are better than Try/Finally blocks: not even a ThreadException will interupt this processing.
					// A CER is not the same as ExecuteCodeWithGuaranteedCleanup: a CER does not have a cleanup.

					// Create an unencrypted version of the SecureString in unmanaged memory (as a BSTR), and get a pointer to it
					Action getBstr = () => insecurePointer = Marshal.SecureStringToBSTR(_secureString);
					getBstr.ExecuteInConstrainedRegion();

					// Get the number of bytes in the BSTR (which are contained in the 4 bytes preceeding the pointer)
					_byteCount = Marshal.ReadInt32(insecurePointer, -4);

					// Create a new buffer of appropriate length that is filled with 0's
					Value = InitializeBuffer(_secureString.Length, _byteCount);

					// It is essential that this buffer not be copied around, even by the CLR garbage collector!
					// To enforce this, we need to "pin" the variable to its current location in managed memory, so it cannot be changed until we release it.
					Action pinValue = () => _gcHandle = GCHandle.Alloc(Value, GCHandleType.Pinned);
					pinValue.ExecuteInConstrainedRegion();

					// Copy the data from the unencrypted BSTR to the buffer
					Buffer.MemoryCopy((void*)insecurePointer, (void*)_gcHandle.AddrOfPinnedObject(), _byteCount, _byteCount);
				}

				// This delegate defines the code that will perform cleanup work (similar to the code in a Finally block)
				void CleanupCode(object userData, bool exceptionThrown)
				{
					// insecurePointer was temporarily used to access the SecureString.
					// Set the BSTR to all 0's and then clean it up. This is important!
					// This prevents sniffers from seeing the sensitive info as it is cleaned up.
					if (insecurePointer != IntPtr.Zero)
					{
						Marshal.ZeroFreeBSTR(insecurePointer);
					}
				}

				// Better than a Try/Finally: Not even a ThreadException will bypass the cleanup code
				RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(TryCode, CleanupCode, null);
			}
		}
	}
}
