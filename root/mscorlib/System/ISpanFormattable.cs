using System;

namespace System
{
	// Token: 0x0200010B RID: 267
	internal interface ISpanFormattable
	{
		// Token: 0x06000A38 RID: 2616
		bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider);
	}
}
