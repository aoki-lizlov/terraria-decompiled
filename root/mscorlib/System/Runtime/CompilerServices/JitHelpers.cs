using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200081B RID: 2075
	internal static class JitHelpers
	{
		// Token: 0x0600465D RID: 18013 RVA: 0x000E6C09 File Offset: 0x000E4E09
		internal static T UnsafeCast<T>(object o) where T : class
		{
			return Array.UnsafeMov<object, T>(o);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000E6C11 File Offset: 0x000E4E11
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, int>(val);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000E6C19 File Offset: 0x000E4E19
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, long>(val);
		}
	}
}
