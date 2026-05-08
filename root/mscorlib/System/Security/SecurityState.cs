using System;

namespace System.Security
{
	// Token: 0x020003BC RID: 956
	public abstract class SecurityState
	{
		// Token: 0x06002928 RID: 10536 RVA: 0x000025BE File Offset: 0x000007BE
		protected SecurityState()
		{
		}

		// Token: 0x06002929 RID: 10537
		public abstract void EnsureState();

		// Token: 0x0600292A RID: 10538 RVA: 0x00096790 File Offset: 0x00094990
		public bool IsStateAvailable()
		{
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			return domainManager != null && domainManager.CheckSecuritySettings(this);
		}
	}
}
