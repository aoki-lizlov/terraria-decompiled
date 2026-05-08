using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		// Token: 0x0600138E RID: 5006 RVA: 0x0004EF25 File Offset: 0x0004D125
		public AppDomainUnloadedException()
			: base("Attempted to access an unloaded AppDomain.")
		{
			base.HResult = -2146234348;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004EF3D File Offset: 0x0004D13D
		public AppDomainUnloadedException(string message)
			: base(message)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004EF51 File Offset: 0x0004D151
		public AppDomainUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000183F5 File Offset: 0x000165F5
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400132E RID: 4910
		internal const int COR_E_APPDOMAINUNLOADED = -2146234348;
	}
}
