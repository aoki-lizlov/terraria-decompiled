using System;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x020008DF RID: 2271
	internal struct RefEmitPermissionSet
	{
		// Token: 0x06004DFA RID: 19962 RVA: 0x000F6016 File Offset: 0x000F4216
		public RefEmitPermissionSet(SecurityAction action, string pset)
		{
			this.action = action;
			this.pset = pset;
		}

		// Token: 0x04003055 RID: 12373
		public SecurityAction action;

		// Token: 0x04003056 RID: 12374
		public string pset;
	}
}
