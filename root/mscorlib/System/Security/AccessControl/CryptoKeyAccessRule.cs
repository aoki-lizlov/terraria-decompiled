using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004E9 RID: 1257
	public sealed class CryptoKeyAccessRule : AccessRule
	{
		// Token: 0x06003356 RID: 13142 RVA: 0x000BD944 File Offset: 0x000BBB44
		public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
			: base(identity, (int)cryptoKeyRights, false, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow)
		{
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000BD952 File Offset: 0x000BBB52
		public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
			: this(new NTAccount(identity), cryptoKeyRights, type)
		{
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000BD962 File Offset: 0x000BBB62
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return (CryptoKeyRights)base.AccessMask;
			}
		}
	}
}
