using System;
using System.Reflection;

namespace CsvHelper.Configuration
{
	// Token: 0x0200003E RID: 62
	public class CsvPropertyReferenceMap
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007BC3 File Offset: 0x00005DC3
		[Obsolete("This property is deprecated and will be removed in the next major release. Use Data.Property instead.", false)]
		public PropertyInfo Property
		{
			get
			{
				return this.data.Property;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00007BD0 File Offset: 0x00005DD0
		[Obsolete("This property is deprecated and will be removed in the next major release. Use Data.Mapping instead.", false)]
		public CsvClassMap Mapping
		{
			get
			{
				return this.data.Mapping;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007BDD File Offset: 0x00005DDD
		public CsvPropertyReferenceMapData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007BE5 File Offset: 0x00005DE5
		public CsvPropertyReferenceMap(PropertyInfo property, CsvClassMap mapping)
		{
			if (mapping == null)
			{
				throw new ArgumentNullException("mapping");
			}
			this.data = new CsvPropertyReferenceMapData(property, mapping);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007C08 File Offset: 0x00005E08
		public CsvPropertyReferenceMap Prefix(string prefix = null)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				prefix = this.data.Property.Name + ".";
			}
			this.data.Prefix = prefix;
			return this;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007C3B File Offset: 0x00005E3B
		internal int GetMaxIndex()
		{
			return this.data.Mapping.GetMaxIndex();
		}

		// Token: 0x04000073 RID: 115
		private readonly CsvPropertyReferenceMapData data;
	}
}
