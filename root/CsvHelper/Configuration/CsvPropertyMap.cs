using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper.TypeConversion;

namespace CsvHelper.Configuration
{
	// Token: 0x02000039 RID: 57
	[DebuggerDisplay("Names = {string.Join(\",\", Data.Names)}, Index = {Data.Index}, Ignore = {Data.Ignore}, Property = {Data.Property}, TypeConverter = {Data.TypeConverter}")]
	public class CsvPropertyMap
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000764A File Offset: 0x0000584A
		public CsvPropertyMapData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007652 File Offset: 0x00005852
		public CsvPropertyMap(PropertyInfo property)
		{
			this.data = new CsvPropertyMapData(property)
			{
				TypeConverter = TypeConverterFactory.GetConverter(property.PropertyType)
			};
			this.data.Names.Add(property.Name);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007690 File Offset: 0x00005890
		public virtual CsvPropertyMap Name(params string[] names)
		{
			if (names == null || names.Length == 0)
			{
				throw new ArgumentNullException("names");
			}
			this.data.Names.Clear();
			this.data.Names.AddRange(names);
			this.data.IsNameSet = true;
			return this;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000076DD File Offset: 0x000058DD
		public virtual CsvPropertyMap NameIndex(int index)
		{
			this.data.NameIndex = index;
			return this;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000076EC File Offset: 0x000058EC
		public virtual CsvPropertyMap Index(int index)
		{
			this.data.Index = index;
			this.data.IsIndexSet = true;
			return this;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007707 File Offset: 0x00005907
		public virtual CsvPropertyMap Ignore()
		{
			this.data.Ignore = true;
			return this;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007716 File Offset: 0x00005916
		public virtual CsvPropertyMap Ignore(bool ignore)
		{
			this.data.Ignore = ignore;
			return this;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007725 File Offset: 0x00005925
		public virtual CsvPropertyMap Default(object defaultValue)
		{
			this.data.Default = defaultValue;
			return this;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007734 File Offset: 0x00005934
		public virtual CsvPropertyMap TypeConverter(ITypeConverter typeConverter)
		{
			this.data.TypeConverter = typeConverter;
			return this;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007743 File Offset: 0x00005943
		public virtual CsvPropertyMap TypeConverter<T>() where T : ITypeConverter
		{
			this.TypeConverter(ReflectionHelper.CreateInstance<T>(new object[0]));
			return this;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007760 File Offset: 0x00005960
		public virtual CsvPropertyMap ConvertUsing<T>(Func<ICsvReaderRow, T> convertExpression)
		{
			this.data.ConvertExpression = (ICsvReaderRow x) => convertExpression(x);
			return this;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000077DD File Offset: 0x000059DD
		public virtual CsvPropertyMap TypeConverterOption(CultureInfo cultureInfo)
		{
			this.data.TypeConverterOptions.CultureInfo = cultureInfo;
			return this;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000077F1 File Offset: 0x000059F1
		public virtual CsvPropertyMap TypeConverterOption(DateTimeStyles dateTimeStyle)
		{
			this.data.TypeConverterOptions.DateTimeStyle = new DateTimeStyles?(dateTimeStyle);
			return this;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000780A File Offset: 0x00005A0A
		public virtual CsvPropertyMap TypeConverterOption(NumberStyles numberStyle)
		{
			this.data.TypeConverterOptions.NumberStyle = new NumberStyles?(numberStyle);
			return this;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007823 File Offset: 0x00005A23
		public virtual CsvPropertyMap TypeConverterOption(string format)
		{
			this.data.TypeConverterOptions.Format = format;
			return this;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007837 File Offset: 0x00005A37
		public virtual CsvPropertyMap TypeConverterOption(bool isTrue, params string[] booleanValues)
		{
			return this.TypeConverterOption(isTrue, true, booleanValues);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007844 File Offset: 0x00005A44
		public virtual CsvPropertyMap TypeConverterOption(bool isTrue, bool clearValues, params string[] booleanValues)
		{
			if (isTrue)
			{
				if (clearValues)
				{
					this.data.TypeConverterOptions.BooleanTrueValues.Clear();
				}
				this.data.TypeConverterOptions.BooleanTrueValues.AddRange(booleanValues);
			}
			else
			{
				if (clearValues)
				{
					this.data.TypeConverterOptions.BooleanFalseValues.Clear();
				}
				this.data.TypeConverterOptions.BooleanFalseValues.AddRange(booleanValues);
			}
			return this;
		}

		// Token: 0x04000062 RID: 98
		private readonly CsvPropertyMapData data;

		// Token: 0x0200004F RID: 79
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0<T>
		{
			// Token: 0x06000271 RID: 625 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x04000098 RID: 152
			public Func<ICsvReaderRow, T> convertExpression;
		}
	}
}
