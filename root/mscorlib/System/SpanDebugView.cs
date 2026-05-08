using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x02000146 RID: 326
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00035439 File Offset: 0x00033639
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003544E File Offset: 0x0003364E
		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00035463 File Offset: 0x00033663
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x0400116B RID: 4459
		private readonly T[] _array;
	}
}
