using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020002A6 RID: 678
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		// Token: 0x06001FA9 RID: 8105 RVA: 0x00074AF7 File Offset: 0x00072CF7
		public ThreadInterruptedException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00074B10 File Offset: 0x00072D10
		public ThreadInterruptedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00074B24 File Offset: 0x00072D24
		public ThreadInterruptedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000183F5 File Offset: 0x000165F5
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
