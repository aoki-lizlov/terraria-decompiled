using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000027 RID: 39
	public class Int64Converter : DefaultTypeConverter
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00005F9C File Offset: 0x0000419C
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			long num;
			if (long.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005B96 File Offset: 0x00003D96
		public Int64Converter()
		{
		}
	}
}
