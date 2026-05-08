using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002FF RID: 767
	[StructLayout(LayoutKind.Auto)]
	internal struct IndexRange
	{
		// Token: 0x04001B0D RID: 6925
		internal long _nFromInclusive;

		// Token: 0x04001B0E RID: 6926
		internal long _nToExclusive;

		// Token: 0x04001B0F RID: 6927
		internal volatile Box<long> _nSharedCurrentIndexOffset;

		// Token: 0x04001B10 RID: 6928
		internal int _bRangeFinished;
	}
}
