using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003E8 RID: 1000
	internal class MonoTrustManager : IApplicationTrustManager, ISecurityEncodable
	{
		// Token: 0x06002A6C RID: 10860 RVA: 0x0009A8F0 File Offset: 0x00098AF0
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
		public ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			return null;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0009A901 File Offset: 0x00098B01
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e.Tag != "IApplicationTrustManager")
			{
				throw new ArgumentException("e", Locale.GetText("Invalid XML tag."));
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0009A938 File Offset: 0x00098B38
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
			securityElement.AddAttribute("class", typeof(MonoTrustManager).AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000025BE File Offset: 0x000007BE
		public MonoTrustManager()
		{
		}

		// Token: 0x04001E6B RID: 7787
		private const string tag = "IApplicationTrustManager";
	}
}
