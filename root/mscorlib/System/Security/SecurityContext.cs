using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
	// Token: 0x020003AC RID: 940
	public sealed class SecurityContext : IDisposable
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x000025BE File Offset: 0x000007BE
		private SecurityContext()
		{
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000025CE File Offset: 0x000007CE
		public SecurityContext CreateCopy()
		{
			return this;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000931B8 File Offset: 0x000913B8
		public static SecurityContext Capture()
		{
			return new SecurityContext();
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0000408A File Offset: 0x0000228A
		public static bool IsFlowSuppressed()
		{
			return false;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0000408A File Offset: 0x0000228A
		public static bool IsWindowsIdentityFlowSuppressed()
		{
			return false;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00004088 File Offset: 0x00002288
		public static void RestoreFlow()
		{
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000931BF File Offset: 0x000913BF
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
		{
			callback(state);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00047E00 File Offset: 0x00046000
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public static AsyncFlowControl SuppressFlow()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00047E00 File Offset: 0x00046000
		public static AsyncFlowControl SuppressFlowWindowsIdentity()
		{
			throw new NotSupportedException();
		}
	}
}
