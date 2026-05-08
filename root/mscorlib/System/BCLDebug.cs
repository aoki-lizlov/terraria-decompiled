using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x020001EF RID: 495
	internal static class BCLDebug
	{
		// Token: 0x060017F3 RID: 6131 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		public static void Assert(bool condition, string message)
		{
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		internal static void Correctness(bool expr, string msg)
		{
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		public static void Log(string message)
		{
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		public static void Log(string switchName, string message)
		{
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		public static void Log(string switchName, BCLDebugLogLevel level, params object[] messages)
		{
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		internal static void Perf(bool expr, string msg)
		{
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_LOGGING")]
		public static void Trace(string switchName, params object[] messages)
		{
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool CheckEnabled(string switchName)
		{
			return false;
		}
	}
}
