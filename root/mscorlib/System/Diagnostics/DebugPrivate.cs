using System;

namespace System.Diagnostics
{
	// Token: 0x02000A17 RID: 2583
	internal static class DebugPrivate
	{
		// Token: 0x06005FCD RID: 24525 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public static void Fail(string message)
		{
		}

		// Token: 0x06005FD2 RID: 24530 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		public static void Fail(string message, string detailMessage)
		{
		}
	}
}
