using System;
using System.Threading;

namespace System
{
	// Token: 0x02000119 RID: 281
	internal sealed class LazyDebugView<T>
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x0002A741 File Offset: 0x00028941
		public LazyDebugView(Lazy<T> lazy)
		{
			this._lazy = lazy;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0002A750 File Offset: 0x00028950
		public bool IsValueCreated
		{
			get
			{
				return this._lazy.IsValueCreated;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002A75D File Offset: 0x0002895D
		public T Value
		{
			get
			{
				return this._lazy.ValueForDebugDisplay;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002A76A File Offset: 0x0002896A
		public LazyThreadSafetyMode? Mode
		{
			get
			{
				return this._lazy.Mode;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002A777 File Offset: 0x00028977
		public bool IsValueFaulted
		{
			get
			{
				return this._lazy.IsValueFaulted;
			}
		}

		// Token: 0x040010F3 RID: 4339
		private readonly Lazy<T> _lazy;
	}
}
