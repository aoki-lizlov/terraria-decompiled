using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace System.Security.Cryptography
{
	// Token: 0x02000453 RID: 1107
	[ComVisible(true)]
	[Serializable]
	public class CryptographicException : SystemException
	{
		// Token: 0x06002E37 RID: 11831 RVA: 0x000A6993 File Offset: 0x000A4B93
		public CryptographicException()
			: base(Environment.GetResourceString("Error occurred during a cryptographic operation."))
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000A69B0 File Offset: 0x000A4BB0
		public CryptographicException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000A69C4 File Offset: 0x000A4BC4
		public CryptographicException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000A69E3 File Offset: 0x000A4BE3
		public CryptographicException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000A69F8 File Offset: 0x000A4BF8
		[SecuritySafeCritical]
		public CryptographicException(int hr)
			: this(Win32Native.GetMessage(hr))
		{
			if (((long)hr & (long)((ulong)(-2147483648))) != (long)((ulong)(-2147483648)))
			{
				hr = (hr & 65535) | -2147024896;
			}
			base.SetErrorCode(hr);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000183F5 File Offset: 0x000165F5
		protected CryptographicException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000A6A2D File Offset: 0x000A4C2D
		private static void ThrowCryptographicException(int hr)
		{
			throw new CryptographicException(hr);
		}

		// Token: 0x0400200D RID: 8205
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x0400200E RID: 8206
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x0400200F RID: 8207
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
	}
}
