using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002A5 RID: 677
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06001FA6 RID: 8102 RVA: 0x00074AD2 File Offset: 0x00072CD2
		private ThreadAbortException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000183F5 File Offset: 0x000165F5
		internal ThreadAbortException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x00074AEB File Offset: 0x00072CEB
		public object ExceptionState
		{
			[SecuritySafeCritical]
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
