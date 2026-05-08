using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000148 RID: 328
	internal struct LzxConstants
	{
		// Token: 0x04000AD4 RID: 2772
		public const ushort MIN_MATCH = 2;

		// Token: 0x04000AD5 RID: 2773
		public const ushort MAX_MATCH = 257;

		// Token: 0x04000AD6 RID: 2774
		public const ushort NUM_CHARS = 256;

		// Token: 0x04000AD7 RID: 2775
		public const ushort PRETREE_NUM_ELEMENTS = 20;

		// Token: 0x04000AD8 RID: 2776
		public const ushort ALIGNED_NUM_ELEMENTS = 8;

		// Token: 0x04000AD9 RID: 2777
		public const ushort NUM_PRIMARY_LENGTHS = 7;

		// Token: 0x04000ADA RID: 2778
		public const ushort NUM_SECONDARY_LENGTHS = 249;

		// Token: 0x04000ADB RID: 2779
		public const ushort PRETREE_MAXSYMBOLS = 20;

		// Token: 0x04000ADC RID: 2780
		public const ushort PRETREE_TABLEBITS = 6;

		// Token: 0x04000ADD RID: 2781
		public const ushort MAINTREE_MAXSYMBOLS = 656;

		// Token: 0x04000ADE RID: 2782
		public const ushort MAINTREE_TABLEBITS = 12;

		// Token: 0x04000ADF RID: 2783
		public const ushort LENGTH_MAXSYMBOLS = 250;

		// Token: 0x04000AE0 RID: 2784
		public const ushort LENGTH_TABLEBITS = 12;

		// Token: 0x04000AE1 RID: 2785
		public const ushort ALIGNED_MAXSYMBOLS = 8;

		// Token: 0x04000AE2 RID: 2786
		public const ushort ALIGNED_TABLEBITS = 7;

		// Token: 0x04000AE3 RID: 2787
		public const ushort LENTABLE_SAFETY = 64;

		// Token: 0x020003E3 RID: 995
		public enum BLOCKTYPE
		{
			// Token: 0x04001E0E RID: 7694
			INVALID,
			// Token: 0x04001E0F RID: 7695
			VERBATIM,
			// Token: 0x04001E10 RID: 7696
			ALIGNED,
			// Token: 0x04001E11 RID: 7697
			UNCOMPRESSED
		}
	}
}
