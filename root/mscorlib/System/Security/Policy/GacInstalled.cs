using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003E0 RID: 992
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06002A31 RID: 10801 RVA: 0x00097F72 File Offset: 0x00096172
		public GacInstalled()
		{
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x0009A197 File Offset: 0x00098397
		public object Copy()
		{
			return new GacInstalled();
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x0009A19E File Offset: 0x0009839E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0009A1A5 File Offset: 0x000983A5
		public override bool Equals(object o)
		{
			return o != null && o is GacInstalled;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0000408A File Offset: 0x0000228A
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0009A1B5 File Offset: 0x000983B5
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement.ToString();
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00003FB7 File Offset: 0x000021B7
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 1;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000887DF File Offset: 0x000869DF
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return position;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0009A1DC File Offset: 0x000983DC
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position] = '\t';
			return position + 1;
		}
	}
}
