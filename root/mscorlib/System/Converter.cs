using System;

namespace System
{
	// Token: 0x020000B6 RID: 182
	// (Invoke) Token: 0x06000512 RID: 1298
	public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
}
