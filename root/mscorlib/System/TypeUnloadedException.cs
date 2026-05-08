using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00048863 File Offset: 0x00046A63
		public TypeUnloadedException()
			: base("Type had been unloaded.")
		{
			base.HResult = -2146234349;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004887B File Offset: 0x00046A7B
		public TypeUnloadedException(string message)
			: base(message)
		{
			base.HResult = -2146234349;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004888F File Offset: 0x00046A8F
		public TypeUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146234349;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000183F5 File Offset: 0x000165F5
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
