using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000555 RID: 1365
	[ComVisible(true)]
	public interface ISponsor
	{
		// Token: 0x060036F5 RID: 14069
		TimeSpan Renewal(ILease lease);
	}
}
