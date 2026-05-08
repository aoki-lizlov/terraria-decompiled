using System;
using System.Collections.Generic;

namespace CsvHelper.Configuration
{
	// Token: 0x0200003B RID: 59
	internal class CsvPropertyMapComparer : IComparer<CsvPropertyMap>
	{
		// Token: 0x0600020E RID: 526 RVA: 0x000079C0 File Offset: 0x00005BC0
		public virtual int Compare(object x, object y)
		{
			CsvPropertyMap csvPropertyMap = x as CsvPropertyMap;
			CsvPropertyMap csvPropertyMap2 = y as CsvPropertyMap;
			return this.Compare(csvPropertyMap, csvPropertyMap2);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000079E4 File Offset: 0x00005BE4
		public virtual int Compare(CsvPropertyMap x, CsvPropertyMap y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			return x.Data.Index.CompareTo(y.Data.Index);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00002253 File Offset: 0x00000453
		public CsvPropertyMapComparer()
		{
		}
	}
}
