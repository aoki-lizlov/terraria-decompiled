using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000124 RID: 292
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		public MulticastNotSupportedException()
			: base("Attempted to add multiple callbacks to a delegate that does not support multicast.")
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		public MulticastNotSupportedException(string message)
			: base(message)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		public MulticastNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000183F5 File Offset: 0x000165F5
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
