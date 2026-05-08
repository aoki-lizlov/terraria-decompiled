using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000180 RID: 384
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00048FE4 File Offset: 0x000471E4
		public UnauthorizedAccessException()
			: base("Attempted to perform an unauthorized operation.")
		{
			base.HResult = -2147024891;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00048FFC File Offset: 0x000471FC
		public UnauthorizedAccessException(string message)
			: base(message)
		{
			base.HResult = -2147024891;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00049010 File Offset: 0x00047210
		public UnauthorizedAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147024891;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000183F5 File Offset: 0x000165F5
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
