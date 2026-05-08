using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003D2 RID: 978
	[ComVisible(true)]
	public static class ApplicationSecurityManager
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x000982F9 File Offset: 0x000964F9
		public static IApplicationTrustManager ApplicationTrustManager
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if (ApplicationSecurityManager._appTrustManager == null)
				{
					ApplicationSecurityManager._appTrustManager = new MonoTrustManager();
				}
				return ApplicationSecurityManager._appTrustManager;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x00098311 File Offset: 0x00096511
		public static ApplicationTrustCollection UserApplicationTrusts
		{
			get
			{
				if (ApplicationSecurityManager._userAppTrusts == null)
				{
					ApplicationSecurityManager._userAppTrusts = new ApplicationTrustCollection();
				}
				return ApplicationSecurityManager._userAppTrusts;
			}
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x00098329 File Offset: 0x00096529
		[MonoTODO("Missing application manifest support")]
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true, ControlEvidence = true)]
		public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new NullReferenceException("activationContext");
			}
			return ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context).IsApplicationTrustedToRun;
		}

		// Token: 0x04001E24 RID: 7716
		private static IApplicationTrustManager _appTrustManager;

		// Token: 0x04001E25 RID: 7717
		private static ApplicationTrustCollection _userAppTrusts;
	}
}
