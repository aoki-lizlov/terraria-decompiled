using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CsvHelper.Configuration
{
	// Token: 0x0200003F RID: 63
	public class CsvPropertyReferenceMapData
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007C4D File Offset: 0x00005E4D
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00007C58 File Offset: 0x00005E58
		public virtual string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
				foreach (CsvPropertyMap csvPropertyMap in this.Mapping.PropertyMaps)
				{
					csvPropertyMap.Data.Names.Prefix = value;
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00007CBC File Offset: 0x00005EBC
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public virtual PropertyInfo Property
		{
			[CompilerGenerated]
			get
			{
				return this.<Property>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Property>k__BackingField = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00007CCD File Offset: 0x00005ECD
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00007CD5 File Offset: 0x00005ED5
		public CsvClassMap Mapping
		{
			[CompilerGenerated]
			get
			{
				return this.<Mapping>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Mapping>k__BackingField = value;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007CDE File Offset: 0x00005EDE
		public CsvPropertyReferenceMapData(PropertyInfo property, CsvClassMap mapping)
		{
			this.Property = property;
			this.Mapping = mapping;
		}

		// Token: 0x04000074 RID: 116
		private string prefix;

		// Token: 0x04000075 RID: 117
		[CompilerGenerated]
		private PropertyInfo <Property>k__BackingField;

		// Token: 0x04000076 RID: 118
		[CompilerGenerated]
		private CsvClassMap <Mapping>k__BackingField;
	}
}
