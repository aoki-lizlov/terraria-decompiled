using System;

namespace System.Security
{
	// Token: 0x02000399 RID: 921
	public interface IStackWalk
	{
		// Token: 0x0600280C RID: 10252
		void Assert();

		// Token: 0x0600280D RID: 10253
		void Demand();

		// Token: 0x0600280E RID: 10254
		void Deny();

		// Token: 0x0600280F RID: 10255
		void PermitOnly();
	}
}
