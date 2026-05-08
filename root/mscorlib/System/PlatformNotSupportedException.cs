using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x000334DE File Offset: 0x000316DE
		public PlatformNotSupportedException()
			: base("Operation is not supported on this platform.")
		{
			base.HResult = -2146233031;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000334F6 File Offset: 0x000316F6
		public PlatformNotSupportedException(string message)
			: base(message)
		{
			base.HResult = -2146233031;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0003350A File Offset: 0x0003170A
		public PlatformNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233031;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0003351F File Offset: 0x0003171F
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
