using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x02000530 RID: 1328
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600357E RID: 13694
		// (set) Token: 0x0600357F RID: 13695
		string TypeName { get; set; }

		// Token: 0x06003580 RID: 13696
		bool CanCastTo(Type fromType, object o);
	}
}
