using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F2 RID: 1266
	public sealed class EventWaitHandleAuditRule : AuditRule
	{
		// Token: 0x060033A3 RID: 13219 RVA: 0x000BE31A File Offset: 0x000BC51A
		public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags)
			: base(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
			if (eventRights < EventWaitHandleRights.Modify || eventRights > EventWaitHandleRights.FullControl)
			{
				throw new ArgumentOutOfRangeException("eventRights");
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000BD962 File Offset: 0x000BBB62
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
