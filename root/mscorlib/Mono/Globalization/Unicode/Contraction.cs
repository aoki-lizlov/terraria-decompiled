using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200004D RID: 77
	internal class Contraction
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000573D File Offset: 0x0000393D
		public Contraction(int index, char[] source, string replacement, byte[] sortkey)
		{
			this.Index = index;
			this.Source = source;
			this.Replacement = replacement;
			this.SortKey = sortkey;
		}

		// Token: 0x04000D34 RID: 3380
		public int Index;

		// Token: 0x04000D35 RID: 3381
		public readonly char[] Source;

		// Token: 0x04000D36 RID: 3382
		public readonly string Replacement;

		// Token: 0x04000D37 RID: 3383
		public readonly byte[] SortKey;
	}
}
