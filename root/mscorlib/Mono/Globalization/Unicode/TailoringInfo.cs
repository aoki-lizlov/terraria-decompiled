using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200004C RID: 76
	internal class TailoringInfo
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005718 File Offset: 0x00003918
		public TailoringInfo(int lcid, int tailoringIndex, int tailoringCount, bool frenchSort)
		{
			this.LCID = lcid;
			this.TailoringIndex = tailoringIndex;
			this.TailoringCount = tailoringCount;
			this.FrenchSort = frenchSort;
		}

		// Token: 0x04000D30 RID: 3376
		public readonly int LCID;

		// Token: 0x04000D31 RID: 3377
		public readonly int TailoringIndex;

		// Token: 0x04000D32 RID: 3378
		public readonly int TailoringCount;

		// Token: 0x04000D33 RID: 3379
		public readonly bool FrenchSort;
	}
}
