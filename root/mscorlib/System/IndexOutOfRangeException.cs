using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x000298A1 File Offset: 0x00027AA1
		public IndexOutOfRangeException()
			: base("Index was outside the bounds of the array.")
		{
			base.HResult = -2146233080;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000298B9 File Offset: 0x00027AB9
		public IndexOutOfRangeException(string message)
			: base(message)
		{
			base.HResult = -2146233080;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000298CD File Offset: 0x00027ACD
		public IndexOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233080;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000183F5 File Offset: 0x000165F5
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
