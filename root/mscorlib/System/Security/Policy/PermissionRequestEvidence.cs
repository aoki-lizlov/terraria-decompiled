using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003EA RID: 1002
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionRequestEvidence : EvidenceBase, IBuiltInEvidence
	{
		// Token: 0x06002A81 RID: 10881 RVA: 0x0009AE52 File Offset: 0x00099052
		public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
		{
			if (request != null)
			{
				this.requested = new PermissionSet(request);
			}
			if (optional != null)
			{
				this.optional = new PermissionSet(optional);
			}
			if (denied != null)
			{
				this.denied = new PermissionSet(denied);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x0009AE87 File Offset: 0x00099087
		public PermissionSet DeniedPermissions
		{
			get
			{
				return this.denied;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0009AE8F File Offset: 0x0009908F
		public PermissionSet OptionalPermissions
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x0009AE97 File Offset: 0x00099097
		public PermissionSet RequestedPermissions
		{
			get
			{
				return this.requested;
			}
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x0009AE9F File Offset: 0x0009909F
		public PermissionRequestEvidence Copy()
		{
			return new PermissionRequestEvidence(this.requested, this.optional, this.denied);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x0009AEB8 File Offset: 0x000990B8
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
			securityElement.AddAttribute("version", "1");
			if (this.requested != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Request");
				securityElement2.AddChild(this.requested.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.optional != null)
			{
				SecurityElement securityElement3 = new SecurityElement("Optional");
				securityElement3.AddChild(this.optional.ToXml());
				securityElement.AddChild(securityElement3);
			}
			if (this.denied != null)
			{
				SecurityElement securityElement4 = new SecurityElement("Denied");
				securityElement4.AddChild(this.denied.ToXml());
				securityElement.AddChild(securityElement4);
			}
			return securityElement.ToString();
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x0009AF68 File Offset: 0x00099168
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			int num = (verbose ? 3 : 1);
			if (this.requested != null)
			{
				int num2 = this.requested.ToXml().ToString().Length + (verbose ? 5 : 0);
				num += num2;
			}
			if (this.optional != null)
			{
				int num3 = this.optional.ToXml().ToString().Length + (verbose ? 5 : 0);
				num += num3;
			}
			if (this.denied != null)
			{
				int num4 = this.denied.ToXml().ToString().Length + (verbose ? 5 : 0);
				num += num4;
			}
			return num;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x04001E70 RID: 7792
		private PermissionSet requested;

		// Token: 0x04001E71 RID: 7793
		private PermissionSet optional;

		// Token: 0x04001E72 RID: 7794
		private PermissionSet denied;
	}
}
