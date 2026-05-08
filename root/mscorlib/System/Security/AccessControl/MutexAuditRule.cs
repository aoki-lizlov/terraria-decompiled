using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000500 RID: 1280
	public sealed class MutexAuditRule : AuditRule
	{
		// Token: 0x06003425 RID: 13349 RVA: 0x000BD96A File Offset: 0x000BBB6A
		public MutexAuditRule(IdentityReference identity, MutexRights eventRights, AuditFlags flags)
			: base(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000BD962 File Offset: 0x000BBB62
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
