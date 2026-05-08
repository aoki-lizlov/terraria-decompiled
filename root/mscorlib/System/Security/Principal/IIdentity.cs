using System;

namespace System.Security.Principal
{
	// Token: 0x020004AF RID: 1199
	public interface IIdentity
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600318E RID: 12686
		string Name { get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600318F RID: 12687
		string AuthenticationType { get; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06003190 RID: 12688
		bool IsAuthenticated { get; }
	}
}
