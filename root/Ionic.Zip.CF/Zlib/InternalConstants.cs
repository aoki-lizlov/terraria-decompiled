using System;

namespace Ionic.Zlib
{
	// Token: 0x0200004F RID: 79
	internal static class InternalConstants
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0001AE50 File Offset: 0x00019050
		// Note: this type is marked as 'beforefieldinit'.
		static InternalConstants()
		{
		}

		// Token: 0x040002C2 RID: 706
		internal static readonly int MAX_BITS = 15;

		// Token: 0x040002C3 RID: 707
		internal static readonly int BL_CODES = 19;

		// Token: 0x040002C4 RID: 708
		internal static readonly int D_CODES = 30;

		// Token: 0x040002C5 RID: 709
		internal static readonly int LITERALS = 256;

		// Token: 0x040002C6 RID: 710
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x040002C7 RID: 711
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x040002C8 RID: 712
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x040002C9 RID: 713
		internal static readonly int REP_3_6 = 16;

		// Token: 0x040002CA RID: 714
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x040002CB RID: 715
		internal static readonly int REPZ_11_138 = 18;
	}
}
