using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper.TypeConversion;

namespace CsvHelper.Configuration
{
	// Token: 0x0200003C RID: 60
	public class CsvPropertyMapData
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007A2B File Offset: 0x00005C2B
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00007A33 File Offset: 0x00005C33
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00007A3C File Offset: 0x00005C3C
		public virtual CsvPropertyNameCollection Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00007A44 File Offset: 0x00005C44
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00007A4C File Offset: 0x00005C4C
		public virtual int NameIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<NameIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NameIndex>k__BackingField = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007A55 File Offset: 0x00005C55
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00007A5D File Offset: 0x00005C5D
		public virtual bool IsNameSet
		{
			[CompilerGenerated]
			get
			{
				return this.<IsNameSet>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsNameSet>k__BackingField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00007A66 File Offset: 0x00005C66
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00007A6E File Offset: 0x00005C6E
		public virtual int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00007A77 File Offset: 0x00005C77
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00007A7F File Offset: 0x00005C7F
		public virtual bool IsIndexSet
		{
			[CompilerGenerated]
			get
			{
				return this.<IsIndexSet>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsIndexSet>k__BackingField = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007A88 File Offset: 0x00005C88
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00007A90 File Offset: 0x00005C90
		public virtual ITypeConverter TypeConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeConverter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TypeConverter>k__BackingField = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00007A99 File Offset: 0x00005C99
		public virtual TypeConverterOptions TypeConverterOptions
		{
			get
			{
				return this.typeConverterOptions;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007AA1 File Offset: 0x00005CA1
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00007AA9 File Offset: 0x00005CA9
		public virtual bool Ignore
		{
			[CompilerGenerated]
			get
			{
				return this.<Ignore>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Ignore>k__BackingField = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00007AB2 File Offset: 0x00005CB2
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00007ABA File Offset: 0x00005CBA
		public virtual object Default
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
				this.IsDefaultSet = true;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007ACA File Offset: 0x00005CCA
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00007AD2 File Offset: 0x00005CD2
		public virtual bool IsDefaultSet
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDefaultSet>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsDefaultSet>k__BackingField = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007ADB File Offset: 0x00005CDB
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00007AE3 File Offset: 0x00005CE3
		public virtual Expression ConvertExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<ConvertExpression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConvertExpression>k__BackingField = value;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007AEC File Offset: 0x00005CEC
		public CsvPropertyMapData(PropertyInfo property)
		{
			this.Property = property;
		}

		// Token: 0x04000065 RID: 101
		private readonly CsvPropertyNameCollection names = new CsvPropertyNameCollection();

		// Token: 0x04000066 RID: 102
		private int index = -1;

		// Token: 0x04000067 RID: 103
		private object defaultValue;

		// Token: 0x04000068 RID: 104
		private readonly TypeConverterOptions typeConverterOptions = new TypeConverterOptions();

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private PropertyInfo <Property>k__BackingField;

		// Token: 0x0400006A RID: 106
		[CompilerGenerated]
		private int <NameIndex>k__BackingField;

		// Token: 0x0400006B RID: 107
		[CompilerGenerated]
		private bool <IsNameSet>k__BackingField;

		// Token: 0x0400006C RID: 108
		[CompilerGenerated]
		private bool <IsIndexSet>k__BackingField;

		// Token: 0x0400006D RID: 109
		[CompilerGenerated]
		private ITypeConverter <TypeConverter>k__BackingField;

		// Token: 0x0400006E RID: 110
		[CompilerGenerated]
		private bool <Ignore>k__BackingField;

		// Token: 0x0400006F RID: 111
		[CompilerGenerated]
		private bool <IsDefaultSet>k__BackingField;

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		private Expression <ConvertExpression>k__BackingField;
	}
}
