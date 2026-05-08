using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AAE RID: 2734
	[DebuggerDisplay("Head = {Head}, Tail = {Tail}")]
	[StructLayout(LayoutKind.Explicit, Size = 384)]
	internal struct PaddedHeadAndTail
	{
		// Token: 0x04003B61 RID: 15201
		[FieldOffset(128)]
		public int Head;

		// Token: 0x04003B62 RID: 15202
		[FieldOffset(256)]
		public int Tail;
	}
}
