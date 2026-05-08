using System;

namespace System.Buffers
{
	// Token: 0x02000B27 RID: 2855
	// (Invoke) Token: 0x060068CD RID: 26829
	public delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
}
