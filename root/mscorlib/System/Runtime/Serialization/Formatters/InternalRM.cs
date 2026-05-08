using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x0200064E RID: 1614
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalRM
	{
		// Token: 0x06003D6C RID: 15724 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000D53D8 File Offset: 0x000D35D8
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("SOAP");
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000025BE File Offset: 0x000007BE
		public InternalRM()
		{
		}
	}
}
