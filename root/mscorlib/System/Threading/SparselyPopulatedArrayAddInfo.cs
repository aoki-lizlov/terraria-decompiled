using System;

namespace System.Threading
{
	// Token: 0x02000285 RID: 645
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x00070E3A File Offset: 0x0006F03A
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this._source = source;
			this._index = index;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00070E4A File Offset: 0x0006F04A
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00070E52 File Offset: 0x0006F052
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x0400197C RID: 6524
		private SparselyPopulatedArrayFragment<T> _source;

		// Token: 0x0400197D RID: 6525
		private int _index;
	}
}
