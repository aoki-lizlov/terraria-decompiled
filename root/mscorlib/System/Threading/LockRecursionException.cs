using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	public class LockRecursionException : Exception
	{
		// Token: 0x06001D62 RID: 7522 RVA: 0x0000455D File Offset: 0x0000275D
		public LockRecursionException()
		{
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0002A236 File Offset: 0x00028436
		public LockRecursionException(string message)
			: base(message)
		{
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0002A23F File Offset: 0x0002843F
		public LockRecursionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00018937 File Offset: 0x00016B37
		protected LockRecursionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
