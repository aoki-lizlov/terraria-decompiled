using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x020003AF RID: 943
	[ComVisible(true)]
	[Serializable]
	public class HostSecurityManager
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x000025BE File Offset: 0x000007BE
		public HostSecurityManager()
		{
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public virtual PolicyLevel DomainPolicy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x00093453 File Offset: 0x00091653
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00093458 File Offset: 0x00091658
		public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
		{
			if (applicationEvidence == null)
			{
				throw new ArgumentNullException("applicationEvidence");
			}
			ActivationArguments activationArguments = null;
			foreach (object obj in applicationEvidence)
			{
				activationArguments = obj as ActivationArguments;
				if (activationArguments != null)
				{
					break;
				}
			}
			if (activationArguments == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("No {0} found in {1}."), "ActivationArguments", "Evidence"), "applicationEvidence");
			}
			if (activationArguments.ActivationContext == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("No {0} found in {1}."), "ActivationContext", "ActivationArguments"), "applicationEvidence");
			}
			if (!ApplicationSecurityManager.DetermineApplicationTrust(activationArguments.ActivationContext, context))
			{
				return null;
			}
			if (activationArguments.ApplicationIdentity == null)
			{
				return new ApplicationTrust();
			}
			return new ApplicationTrust(activationArguments.ApplicationIdentity);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000025F2 File Offset: 0x000007F2
		public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
		{
			return inputEvidence;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000887DF File Offset: 0x000869DF
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x00093538 File Offset: 0x00091738
		public virtual PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new NullReferenceException("evidence");
			}
			return SecurityManager.ResolvePolicy(evidence);
		}
	}
}
