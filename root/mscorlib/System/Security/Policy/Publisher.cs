using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	// Token: 0x020003CC RID: 972
	[Serializable]
	public sealed class Publisher : EvidenceBase, IIdentityPermissionFactory
	{
		// Token: 0x06002960 RID: 10592 RVA: 0x00097F72 File Offset: 0x00096172
		public Publisher(X509Certificate cert)
		{
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public X509Certificate Certificate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public object Copy()
		{
			return null;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return null;
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00097F7A File Offset: 0x0009617A
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00093238 File Offset: 0x00091438
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00097F83 File Offset: 0x00096183
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
