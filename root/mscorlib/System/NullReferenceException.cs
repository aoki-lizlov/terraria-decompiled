using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public class NullReferenceException : SystemException
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0002D991 File Offset: 0x0002BB91
		public NullReferenceException()
			: base("Object reference not set to an instance of an object.")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000183CC File Offset: 0x000165CC
		public NullReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000183E0 File Offset: 0x000165E0
		public NullReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000183F5 File Offset: 0x000165F5
		protected NullReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
