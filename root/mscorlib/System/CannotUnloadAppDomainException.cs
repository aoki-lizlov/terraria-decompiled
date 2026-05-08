using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0004F24C File Offset: 0x0004D44C
		public CannotUnloadAppDomainException()
			: base("Attempt to unload the AppDomain failed.")
		{
			base.HResult = -2146234347;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004F264 File Offset: 0x0004D464
		public CannotUnloadAppDomainException(string message)
			: base(message)
		{
			base.HResult = -2146234347;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004F278 File Offset: 0x0004D478
		public CannotUnloadAppDomainException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146234347;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000183F5 File Offset: 0x000165F5
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04001334 RID: 4916
		internal const int COR_E_CANNOTUNLOADAPPDOMAIN = -2146234347;
	}
}
