using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000AC RID: 172
	public class JsonMergeSettings
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00022E0A File Offset: 0x0002100A
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00022E12 File Offset: 0x00021012
		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeArrayHandling = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00022E2E File Offset: 0x0002102E
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00022E36 File Offset: 0x00021036
		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeNullValueHandling = value;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00008020 File Offset: 0x00006220
		public JsonMergeSettings()
		{
		}

		// Token: 0x04000332 RID: 818
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x04000333 RID: 819
		private MergeNullValueHandling _mergeNullValueHandling;
	}
}
