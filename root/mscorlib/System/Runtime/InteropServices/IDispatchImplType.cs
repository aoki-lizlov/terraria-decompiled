using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C2 RID: 1730
	[Obsolete("The IDispatchImplAttribute is deprecated.", false)]
	[ComVisible(true)]
	[Serializable]
	public enum IDispatchImplType
	{
		// Token: 0x040029C2 RID: 10690
		SystemDefinedImpl,
		// Token: 0x040029C3 RID: 10691
		InternalImpl,
		// Token: 0x040029C4 RID: 10692
		CompatibleImpl
	}
}
