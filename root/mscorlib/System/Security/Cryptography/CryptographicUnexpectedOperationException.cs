using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	// Token: 0x02000454 RID: 1108
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		// Token: 0x06002E3E RID: 11838 RVA: 0x000A6A35 File Offset: 0x000A4C35
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000A6A48 File Offset: 0x000A4C48
		public CryptographicUnexpectedOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000A6A5C File Offset: 0x000A4C5C
		public CryptographicUnexpectedOperationException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000A6A7B File Offset: 0x000A4C7B
		public CryptographicUnexpectedOperationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000A6A90 File Offset: 0x000A4C90
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
