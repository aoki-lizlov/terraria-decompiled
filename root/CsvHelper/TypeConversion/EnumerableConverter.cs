using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000023 RID: 35
	public class EnumerableConverter : DefaultTypeConverter
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005ED8 File Offset: 0x000040D8
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			throw new CsvTypeConverterException("Converting IEnumerable types is not supported for a single field. If you want to do this, create your own ITypeConverter and register it in the TypeConverterFactory by calling AddConverter.");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005EE4 File Offset: 0x000040E4
		public override string ConvertToString(TypeConverterOptions options, object value)
		{
			throw new CsvTypeConverterException("Converting IEnumerable types is not supported for a single field. If you want to do this, create your own ITypeConverter and register it in the TypeConverterFactory by calling AddConverter.");
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005EF0 File Offset: 0x000040F0
		public override bool CanConvertFrom(Type type)
		{
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005EF0 File Offset: 0x000040F0
		public override bool CanConvertTo(Type type)
		{
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005B96 File Offset: 0x00003D96
		public EnumerableConverter()
		{
		}
	}
}
