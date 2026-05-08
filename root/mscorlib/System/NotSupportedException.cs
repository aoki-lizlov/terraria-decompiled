using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class NotSupportedException : SystemException
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x0002D950 File Offset: 0x0002BB50
		public NotSupportedException()
			: base("Specified method is not supported.")
		{
			base.HResult = -2146233067;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002D968 File Offset: 0x0002BB68
		public NotSupportedException(string message)
			: base(message)
		{
			base.HResult = -2146233067;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002D97C File Offset: 0x0002BB7C
		public NotSupportedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233067;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000183F5 File Offset: 0x000165F5
		protected NotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
