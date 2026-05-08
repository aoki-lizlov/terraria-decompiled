using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System
{
	// Token: 0x020001FC RID: 508
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Infrastructure = true)]
	public class AppDomainManager : MarshalByRefObject
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x0005E380 File Offset: 0x0005C580
		public AppDomainManager()
		{
			this._flags = AppDomainManagerInitializationOptions.None;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0005E38F File Offset: 0x0005C58F
		public virtual ApplicationActivator ApplicationActivator
		{
			get
			{
				if (this._activator == null)
				{
					this._activator = new ApplicationActivator();
				}
				return this._activator;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0005E3AA File Offset: 0x0005C5AA
		public virtual Assembly EntryAssembly
		{
			get
			{
				return Assembly.GetEntryAssembly();
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual HostExecutionContextManager HostExecutionContextManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public virtual HostSecurityManager HostSecurityManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0005E3B1 File Offset: 0x0005C5B1
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0005E3B9 File Offset: 0x0005C5B9
		public AppDomainManagerInitializationOptions InitializationFlags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
		public virtual AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			this.InitializeNewDomain(appDomainInfo);
			AppDomain appDomain = AppDomainManager.CreateDomainHelper(friendlyName, securityInfo, appDomainInfo);
			if ((this.HostSecurityManager.Flags & HostSecurityManagerOptions.HostPolicyLevel) == HostSecurityManagerOptions.HostPolicyLevel)
			{
				PolicyLevel domainPolicy = this.HostSecurityManager.DomainPolicy;
				if (domainPolicy != null)
				{
					appDomain.SetAppDomainPolicy(domainPolicy);
				}
			}
			return appDomain;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void InitializeNewDomain(AppDomainSetup appDomainInfo)
		{
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool CheckSecuritySettings(SecurityState state)
		{
			return false;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0005E408 File Offset: 0x0005C608
		protected static AppDomain CreateDomainHelper(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainInfo);
		}

		// Token: 0x040015A1 RID: 5537
		private ApplicationActivator _activator;

		// Token: 0x040015A2 RID: 5538
		private AppDomainManagerInitializationOptions _flags;
	}
}
