using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000272 RID: 626
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		// Token: 0x06001D83 RID: 7555 RVA: 0x0006F2A4 File Offset: 0x0006D4A4
		public WaitHandleCannotBeOpenedException()
			: base("No handle of the given name exists.")
		{
			base.HResult = -2146233044;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0006F2BC File Offset: 0x0006D4BC
		public WaitHandleCannotBeOpenedException(string message)
			: base(message)
		{
			base.HResult = -2146233044;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0006F2D0 File Offset: 0x0006D4D0
		public WaitHandleCannotBeOpenedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233044;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0006F2E5 File Offset: 0x0006D4E5
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
