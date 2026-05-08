using System;
using System.Security;

namespace System.Runtime
{
	// Token: 0x02000520 RID: 1312
	public static class ProfileOptimization
	{
		// Token: 0x06003549 RID: 13641 RVA: 0x00004088 File Offset: 0x00002288
		internal static void InternalSetProfileRoot(string directoryPath)
		{
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x00004088 File Offset: 0x00002288
		internal static void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext)
		{
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000C19E8 File Offset: 0x000BFBE8
		[SecurityCritical]
		public static void SetProfileRoot(string directoryPath)
		{
			ProfileOptimization.InternalSetProfileRoot(directoryPath);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000C19F0 File Offset: 0x000BFBF0
		[SecurityCritical]
		public static void StartProfile(string profile)
		{
			ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
		}
	}
}
