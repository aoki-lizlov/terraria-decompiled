using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000769 RID: 1897
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06004479 RID: 17529
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int WindowsCreateString(string sourceString, int length, IntPtr* hstring);

		// Token: 0x0600447A RID: 17530
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int WindowsDeleteString(IntPtr hstring);

		// Token: 0x0600447B RID: 17531
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char* WindowsGetStringRawBuffer(IntPtr hstring, uint* length);

		// Token: 0x0600447C RID: 17532
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RoOriginateLanguageException(int error, string message, IntPtr languageException);

		// Token: 0x0600447D RID: 17533
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

		// Token: 0x0600447E RID: 17534
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IRestrictedErrorInfo GetRestrictedErrorInfo();
	}
}
