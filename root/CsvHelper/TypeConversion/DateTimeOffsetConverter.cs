using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200001E RID: 30
	public class DateTimeOffsetConverter : DefaultTypeConverter
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			if (text == null)
			{
				return base.ConvertFromString(options, null);
			}
			if (text.Trim().Length == 0)
			{
				return DateTimeOffset.MinValue;
			}
			IFormatProvider formatProvider = ((IFormatProvider)options.CultureInfo.GetFormat(typeof(DateTimeFormatInfo))) ?? options.CultureInfo;
			DateTimeStyles dateTimeStyles = options.DateTimeStyle ?? 0;
			return string.IsNullOrEmpty(options.Format) ? DateTimeOffset.Parse(text, formatProvider, dateTimeStyles) : DateTimeOffset.ParseExact(text, options.Format, formatProvider, dateTimeStyles);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005B96 File Offset: 0x00003D96
		public DateTimeOffsetConverter()
		{
		}
	}
}
