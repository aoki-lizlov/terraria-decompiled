using System;

namespace System.Buffers
{
	// Token: 0x02000B28 RID: 2856
	// (Invoke) Token: 0x060068D1 RID: 26833
	public delegate void ReadOnlySpanAction<T, in TArg>(ReadOnlySpan<T> span, TArg arg);
}
