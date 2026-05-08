using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	// Token: 0x02000527 RID: 1319
	[ComVisible(true)]
	[MonoTODO("missing manifest support")]
	public class ApplicationActivator
	{
		// Token: 0x06003561 RID: 13665 RVA: 0x000025BE File Offset: 0x000007BE
		public ApplicationActivator()
		{
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000C1B24 File Offset: 0x000BFD24
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			return this.CreateInstance(activationContext, null);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000C1B2E File Offset: 0x000BFD2E
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			return ApplicationActivator.CreateInstanceHelper(new AppDomainSetup(activationContext));
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000C1B4C File Offset: 0x000BFD4C
		protected static ObjectHandle CreateInstanceHelper(AppDomainSetup adSetup)
		{
			if (adSetup == null)
			{
				throw new ArgumentNullException("adSetup");
			}
			if (adSetup.ActivationArguments == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("{0} is missing it's {1} property"), "AppDomainSetup", "ActivationArguments"), "adSetup");
			}
			HostSecurityManager hostSecurityManager;
			if (AppDomain.CurrentDomain.DomainManager != null)
			{
				hostSecurityManager = AppDomain.CurrentDomain.DomainManager.HostSecurityManager;
			}
			else
			{
				hostSecurityManager = new HostSecurityManager();
			}
			Evidence evidence = new Evidence();
			evidence.AddHost(adSetup.ActivationArguments);
			TrustManagerContext trustManagerContext = new TrustManagerContext();
			if (!hostSecurityManager.DetermineApplicationTrust(evidence, null, trustManagerContext).IsApplicationTrustedToRun)
			{
				throw new PolicyException(Locale.GetText("Current policy doesn't allow execution of addin."));
			}
			return AppDomain.CreateDomain("friendlyName", null, adSetup).CreateInstance("assemblyName", "typeName", null);
		}
	}
}
