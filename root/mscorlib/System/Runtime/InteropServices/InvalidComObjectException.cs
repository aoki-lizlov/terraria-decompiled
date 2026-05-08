using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006AA RID: 1706
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		// Token: 0x06003FCE RID: 16334 RVA: 0x000E0700 File Offset: 0x000DE900
		public InvalidComObjectException()
			: base("Attempt has been made to use a COM object that does not have a backing class factory.")
		{
			base.HResult = -2146233049;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000E0718 File Offset: 0x000DE918
		public InvalidComObjectException(string message)
			: base(message)
		{
			base.HResult = -2146233049;
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000E072C File Offset: 0x000DE92C
		public InvalidComObjectException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233049;
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000183F5 File Offset: 0x000165F5
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
