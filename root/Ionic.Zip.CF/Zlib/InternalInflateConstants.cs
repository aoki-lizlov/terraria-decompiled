using System;

namespace Ionic.Zlib
{
	// Token: 0x02000043 RID: 67
	internal static class InternalInflateConstants
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0001644C File Offset: 0x0001464C
		// Note: this type is marked as 'beforefieldinit'.
		static InternalInflateConstants()
		{
		}

		// Token: 0x0400024C RID: 588
		internal static readonly int[] InflateMask = new int[]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535
		};
	}
}
