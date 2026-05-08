using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004FF RID: 1279
	public sealed class MutexAccessRule : AccessRule
	{
		// Token: 0x06003422 RID: 13346 RVA: 0x000BF288 File Offset: 0x000BD488
		public MutexAccessRule(IdentityReference identity, MutexRights eventRights, AccessControlType type)
			: base(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000BF296 File Offset: 0x000BD496
		public MutexAccessRule(string identity, MutexRights eventRights, AccessControlType type)
			: this(new NTAccount(identity), eventRights, type)
		{
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000BD962 File Offset: 0x000BBB62
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
