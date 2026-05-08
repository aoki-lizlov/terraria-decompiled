using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004EA RID: 1258
	public sealed class CryptoKeyAuditRule : AuditRule
	{
		// Token: 0x06003359 RID: 13145 RVA: 0x000BD96A File Offset: 0x000BBB6A
		public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
			: base(identity, (int)cryptoKeyRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000BD978 File Offset: 0x000BBB78
		public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
			: this(new NTAccount(identity), cryptoKeyRights, flags)
		{
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000BD962 File Offset: 0x000BBB62
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return (CryptoKeyRights)base.AccessMask;
			}
		}
	}
}
