using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F1 RID: 1265
	public sealed class EventWaitHandleAccessRule : AccessRule
	{
		// Token: 0x060033A0 RID: 13216 RVA: 0x000BD944 File Offset: 0x000BBB44
		public EventWaitHandleAccessRule(IdentityReference identity, EventWaitHandleRights eventRights, AccessControlType type)
			: base(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow)
		{
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000BE30A File Offset: 0x000BC50A
		public EventWaitHandleAccessRule(string identity, EventWaitHandleRights eventRights, AccessControlType type)
			: this(new NTAccount(identity), eventRights, type)
		{
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000BD962 File Offset: 0x000BBB62
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
