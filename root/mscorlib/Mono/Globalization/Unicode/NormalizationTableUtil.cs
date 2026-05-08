using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000053 RID: 83
	internal class NormalizationTableUtil
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000065E0 File Offset: 0x000047E0
		static NormalizationTableUtil()
		{
			int[] array = new int[]
			{
				0, 2320, 6912, 9312, 10624, 11376, 11616, 11920, 42864, 42992,
				63744
			};
			int[] array2 = new int[]
			{
				1760, 4608, 9008, 9728, 10976, 11392, 11632, 13312, 42880, 43008,
				65536
			};
			int[] array3 = new int[] { 144, 2336, 7456, 9312, 9376, 10752, 11616, 11920, 63744 };
			int[] array4 = new int[] { 1760, 4352, 9008, 9376, 9456, 10976, 11632, 13312, 65536 };
			int[] array5 = new int[]
			{
				752, 1152, 1424, 2352, 2480, 2608, 2736, 2864, 3008, 3136,
				3248, 3392, 3520, 3632, 3760, 3840, 4144, 4944, 5904, 6096,
				6304, 6448, 6672, 7616, 8400, 12320, 12432, 43008, 64272, 65056
			};
			int[] array6 = new int[]
			{
				864, 1168, 1872, 2400, 2512, 2640, 2768, 2896, 3024, 3168,
				3280, 3408, 3536, 3664, 3792, 4048, 4160, 4960, 5952, 6112,
				6320, 6464, 6688, 7632, 8432, 12336, 12448, 43024, 64288, 65072
			};
			int[] array7 = new int[] { 1152, 5136, 5744 };
			int[] array8 = new int[] { 4224, 5504, 8624 };
			int[] array9 = new int[] { 0, 2304, 7424, 9472, 12288, 15248, 16400, 19968, 64320 };
			int[] array10 = new int[] { 1792, 4608, 8960, 9728, 12640, 15264, 16432, 40960, 64336 };
			NormalizationTableUtil.Prop = new CodePointIndexer(array, array2, 0, 0);
			NormalizationTableUtil.Map = new CodePointIndexer(array3, array4, 0, 0);
			NormalizationTableUtil.Combining = new CodePointIndexer(array5, array6, 0, 0);
			NormalizationTableUtil.Composite = new CodePointIndexer(array7, array8, 0, 0);
			NormalizationTableUtil.Helper = new CodePointIndexer(array9, array10, 0, 0);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000066FB File Offset: 0x000048FB
		public static int PropIdx(int cp)
		{
			return NormalizationTableUtil.Prop.ToIndex(cp);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006708 File Offset: 0x00004908
		public static int PropCP(int index)
		{
			return NormalizationTableUtil.Prop.ToCodePoint(index);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006715 File Offset: 0x00004915
		public static int PropCount
		{
			get
			{
				return NormalizationTableUtil.Prop.TotalCount;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006721 File Offset: 0x00004921
		public static int MapIdx(int cp)
		{
			return NormalizationTableUtil.Map.ToIndex(cp);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000672E File Offset: 0x0000492E
		public static int MapCP(int index)
		{
			return NormalizationTableUtil.Map.ToCodePoint(index);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000673B File Offset: 0x0000493B
		public static int CbIdx(int cp)
		{
			return NormalizationTableUtil.Combining.ToIndex(cp);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006748 File Offset: 0x00004948
		public static int CbCP(int index)
		{
			return NormalizationTableUtil.Combining.ToCodePoint(index);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006755 File Offset: 0x00004955
		public static int MapCount
		{
			get
			{
				return NormalizationTableUtil.Map.TotalCount;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000025BE File Offset: 0x000007BE
		public NormalizationTableUtil()
		{
		}

		// Token: 0x04000D59 RID: 3417
		public static readonly CodePointIndexer Prop;

		// Token: 0x04000D5A RID: 3418
		public static readonly CodePointIndexer Map;

		// Token: 0x04000D5B RID: 3419
		public static readonly CodePointIndexer Combining;

		// Token: 0x04000D5C RID: 3420
		public static readonly CodePointIndexer Composite;

		// Token: 0x04000D5D RID: 3421
		public static readonly CodePointIndexer Helper;
	}
}
