using System;

namespace System.Security
{
	// Token: 0x02000395 RID: 917
	public interface IPermission : ISecurityEncodable
	{
		// Token: 0x06002802 RID: 10242
		IPermission Copy();

		// Token: 0x06002803 RID: 10243
		void Demand();

		// Token: 0x06002804 RID: 10244
		IPermission Intersect(IPermission target);

		// Token: 0x06002805 RID: 10245
		bool IsSubsetOf(IPermission target);

		// Token: 0x06002806 RID: 10246
		IPermission Union(IPermission target);
	}
}
