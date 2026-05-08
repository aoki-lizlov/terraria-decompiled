using System;

namespace System
{
	// Token: 0x0200020C RID: 524
	internal static class EmptyArray<T>
	{
		// Token: 0x060019A5 RID: 6565 RVA: 0x000606A7 File Offset: 0x0005E8A7
		// Note: this type is marked as 'beforefieldinit'.
		static EmptyArray()
		{
		}

		// Token: 0x040015FC RID: 5628
		public static readonly T[] Value = new T[0];
	}
}
