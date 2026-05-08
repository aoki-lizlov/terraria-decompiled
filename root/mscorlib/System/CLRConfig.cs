using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020001F0 RID: 496
	[FriendAccessAllowed]
	internal class CLRConfig
	{
		// Token: 0x060017FB RID: 6139 RVA: 0x0000408A File Offset: 0x0000228A
		[FriendAccessAllowed]
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		internal static bool CheckLegacyManagedDeflateStream()
		{
			return false;
		}

		// Token: 0x060017FC RID: 6140
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckThrowUnobservedTaskExceptions();

		// Token: 0x060017FD RID: 6141 RVA: 0x000025BE File Offset: 0x000007BE
		public CLRConfig()
		{
		}
	}
}
