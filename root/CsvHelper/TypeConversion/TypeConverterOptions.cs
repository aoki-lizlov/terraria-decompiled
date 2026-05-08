using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200002F RID: 47
	public class TypeConverterOptions
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006581 File Offset: 0x00004781
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006589 File Offset: 0x00004789
		public CultureInfo CultureInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CultureInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CultureInfo>k__BackingField = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006592 File Offset: 0x00004792
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000659A File Offset: 0x0000479A
		public DateTimeStyles? DateTimeStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<DateTimeStyle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DateTimeStyle>k__BackingField = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000065A3 File Offset: 0x000047A3
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000065AB File Offset: 0x000047AB
		public TimeSpanStyles? TimeSpanStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<TimeSpanStyle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TimeSpanStyle>k__BackingField = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000065B4 File Offset: 0x000047B4
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000065BC File Offset: 0x000047BC
		public NumberStyles? NumberStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberStyle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NumberStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000065C5 File Offset: 0x000047C5
		public List<string> BooleanTrueValues
		{
			get
			{
				return this.booleanTrueValues;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000065CD File Offset: 0x000047CD
		public List<string> BooleanFalseValues
		{
			get
			{
				return this.booleanFalseValues;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000065D5 File Offset: 0x000047D5
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000065DD File Offset: 0x000047DD
		public string Format
		{
			[CompilerGenerated]
			get
			{
				return this.<Format>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Format>k__BackingField = value;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000065E8 File Offset: 0x000047E8
		public static TypeConverterOptions Merge(params TypeConverterOptions[] sources)
		{
			TypeConverterOptions typeConverterOptions = new TypeConverterOptions();
			foreach (TypeConverterOptions typeConverterOptions2 in sources)
			{
				if (typeConverterOptions2 != null)
				{
					if (typeConverterOptions2.CultureInfo != null)
					{
						typeConverterOptions.CultureInfo = typeConverterOptions2.CultureInfo;
					}
					if (typeConverterOptions2.DateTimeStyle != null)
					{
						typeConverterOptions.DateTimeStyle = typeConverterOptions2.DateTimeStyle;
					}
					if (typeConverterOptions2.TimeSpanStyle != null)
					{
						typeConverterOptions.TimeSpanStyle = typeConverterOptions2.TimeSpanStyle;
					}
					if (typeConverterOptions2.NumberStyle != null)
					{
						typeConverterOptions.NumberStyle = typeConverterOptions2.NumberStyle;
					}
					if (typeConverterOptions2.Format != null)
					{
						typeConverterOptions.Format = typeConverterOptions2.Format;
					}
					if (!Enumerable.SequenceEqual<string>(typeConverterOptions.booleanTrueValues, typeConverterOptions2.booleanTrueValues))
					{
						typeConverterOptions.booleanTrueValues.Clear();
						typeConverterOptions.booleanTrueValues.AddRange(typeConverterOptions2.booleanTrueValues);
					}
					if (!Enumerable.SequenceEqual<string>(typeConverterOptions.booleanFalseValues, typeConverterOptions2.booleanFalseValues))
					{
						typeConverterOptions.booleanFalseValues.Clear();
						typeConverterOptions.booleanFalseValues.AddRange(typeConverterOptions2.booleanFalseValues);
					}
				}
			}
			return typeConverterOptions;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000066FC File Offset: 0x000048FC
		public TypeConverterOptions()
		{
			List<string> list = new List<string>();
			list.Add("yes");
			list.Add("y");
			this.booleanTrueValues = list;
			List<string> list2 = new List<string>();
			list2.Add("no");
			list2.Add("n");
			this.booleanFalseValues = list2;
			base..ctor();
		}

		// Token: 0x04000031 RID: 49
		private readonly List<string> booleanTrueValues;

		// Token: 0x04000032 RID: 50
		private readonly List<string> booleanFalseValues;

		// Token: 0x04000033 RID: 51
		[CompilerGenerated]
		private CultureInfo <CultureInfo>k__BackingField;

		// Token: 0x04000034 RID: 52
		[CompilerGenerated]
		private DateTimeStyles? <DateTimeStyle>k__BackingField;

		// Token: 0x04000035 RID: 53
		[CompilerGenerated]
		private TimeSpanStyles? <TimeSpanStyle>k__BackingField;

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		private NumberStyles? <NumberStyle>k__BackingField;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		private string <Format>k__BackingField;
	}
}
