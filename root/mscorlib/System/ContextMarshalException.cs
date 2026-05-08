using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001C4 RID: 452
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x000543C5 File Offset: 0x000525C5
		public ContextMarshalException()
			: base(Environment.GetResourceString("Attempted to marshal an object across a context boundary."))
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000543E2 File Offset: 0x000525E2
		public ContextMarshalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000543F6 File Offset: 0x000525F6
		public ContextMarshalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233084);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000183F5 File Offset: 0x000165F5
		protected ContextMarshalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
