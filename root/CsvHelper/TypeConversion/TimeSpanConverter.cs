using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200002D RID: 45
	public class TimeSpanConverter : DefaultTypeConverter
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006130 File Offset: 0x00004330
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			IFormatProvider cultureInfo = options.CultureInfo;
			TimeSpanStyles timeSpanStyles = options.TimeSpanStyle ?? 0;
			TimeSpan timeSpan;
			if (!string.IsNullOrEmpty(options.Format) && TimeSpan.TryParseExact(text, options.Format, cultureInfo, timeSpanStyles, ref timeSpan))
			{
				return timeSpan;
			}
			if (string.IsNullOrEmpty(options.Format) && TimeSpan.TryParse(text, cultureInfo, ref timeSpan))
			{
				return timeSpan;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005B96 File Offset: 0x00003D96
		public TimeSpanConverter()
		{
		}
	}
}
