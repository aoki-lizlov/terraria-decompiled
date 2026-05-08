using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DF RID: 223
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x000201AD File Offset: 0x0001E3AD
		public ExecutionEngineException()
			: base("Internal error in the runtime.")
		{
			base.HResult = -2146233082;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000201C5 File Offset: 0x0001E3C5
		public ExecutionEngineException(string message)
			: base(message)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000201D9 File Offset: 0x0001E3D9
		public ExecutionEngineException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000183F5 File Offset: 0x000165F5
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
