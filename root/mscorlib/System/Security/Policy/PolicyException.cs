using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	// Token: 0x020003EB RID: 1003
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x0009AFFC File Offset: 0x000991FC
		public PolicyException()
			: base(Locale.GetText("Cannot run because of policy."))
		{
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		public PolicyException(string message)
			: base(message)
		{
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000183F5 File Offset: 0x000165F5
		protected PolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0006F1D6 File Offset: 0x0006D3D6
		public PolicyException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
