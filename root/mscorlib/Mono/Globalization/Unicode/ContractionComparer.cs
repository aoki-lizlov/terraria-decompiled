using System;
using System.Collections.Generic;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200004E RID: 78
	internal class ContractionComparer : IComparer<Contraction>
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005764 File Offset: 0x00003964
		public int Compare(Contraction c1, Contraction c2)
		{
			char[] source = c1.Source;
			char[] source2 = c2.Source;
			int num = ((source.Length > source2.Length) ? source2.Length : source.Length);
			for (int i = 0; i < num; i++)
			{
				if (source[i] != source2[i])
				{
					return (int)(source[i] - source2[i]);
				}
			}
			if (source.Length != source2.Length)
			{
				return source.Length - source2.Length;
			}
			return c1.Index - c2.Index;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000025BE File Offset: 0x000007BE
		public ContractionComparer()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000057C9 File Offset: 0x000039C9
		// Note: this type is marked as 'beforefieldinit'.
		static ContractionComparer()
		{
		}

		// Token: 0x04000D38 RID: 3384
		public static readonly ContractionComparer Instance = new ContractionComparer();
	}
}
