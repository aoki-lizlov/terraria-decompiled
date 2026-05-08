using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200002C RID: 44
	public class StringConverter : DefaultTypeConverter
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00006124 File Offset: 0x00004324
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005B96 File Offset: 0x00003D96
		public StringConverter()
		{
		}
	}
}
