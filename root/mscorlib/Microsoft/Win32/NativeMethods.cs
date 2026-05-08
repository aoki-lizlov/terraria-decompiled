using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Win32
{
	// Token: 0x02000091 RID: 145
	internal static class NativeMethods
	{
		// Token: 0x06000490 RID: 1168
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentProcessId();
	}
}
