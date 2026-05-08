using System;
using System.Runtime.CompilerServices;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000029 RID: 41
	public class NullableConverter : DefaultTypeConverter
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005FE4 File Offset: 0x000041E4
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005FEC File Offset: 0x000041EC
		public Type NullableType
		{
			[CompilerGenerated]
			get
			{
				return this.<NullableType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NullableType>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005FF5 File Offset: 0x000041F5
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005FFD File Offset: 0x000041FD
		public Type UnderlyingType
		{
			[CompilerGenerated]
			get
			{
				return this.<UnderlyingType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UnderlyingType>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006006 File Offset: 0x00004206
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000600E File Offset: 0x0000420E
		public ITypeConverter UnderlyingTypeConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<UnderlyingTypeConverter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UnderlyingTypeConverter>k__BackingField = value;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006018 File Offset: 0x00004218
		public NullableConverter(Type type)
		{
			this.NullableType = type;
			this.UnderlyingType = Nullable.GetUnderlyingType(type);
			if (this.UnderlyingType == null)
			{
				throw new ArgumentException("type is not a nullable type.");
			}
			this.UnderlyingTypeConverter = TypeConverterFactory.GetConverter(this.UnderlyingType);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006068 File Offset: 0x00004268
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return this.UnderlyingTypeConverter.ConvertFromString(options, text);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006081 File Offset: 0x00004281
		public override string ConvertToString(TypeConverterOptions options, object value)
		{
			return this.UnderlyingTypeConverter.ConvertToString(options, value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		private Type <NullableType>k__BackingField;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private Type <UnderlyingType>k__BackingField;

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		private ITypeConverter <UnderlyingTypeConverter>k__BackingField;
	}
}
