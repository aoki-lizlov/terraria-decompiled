using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	public class AccessViolationException : SystemException
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x000183B4 File Offset: 0x000165B4
		public AccessViolationException()
			: base("Attempted to read or write protected memory. This is often an indication that other memory is corrupt.")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000183CC File Offset: 0x000165CC
		public AccessViolationException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000183E0 File Offset: 0x000165E0
		public AccessViolationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000183F5 File Offset: 0x000165F5
		protected AccessViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000EC7 RID: 3783
		private IntPtr _ip;

		// Token: 0x04000EC8 RID: 3784
		private IntPtr _target;

		// Token: 0x04000EC9 RID: 3785
		private int _accessType;
	}
}
