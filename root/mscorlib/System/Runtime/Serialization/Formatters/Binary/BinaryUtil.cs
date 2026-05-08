using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000663 RID: 1635
	internal static class BinaryUtil
	{
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}
