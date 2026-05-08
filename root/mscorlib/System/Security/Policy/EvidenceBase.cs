using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003DD RID: 989
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class EvidenceBase
	{
		// Token: 0x06002A1C RID: 10780 RVA: 0x000174FB File Offset: 0x000156FB
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		public virtual EvidenceBase Clone()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000025BE File Offset: 0x000007BE
		protected EvidenceBase()
		{
		}
	}
}
