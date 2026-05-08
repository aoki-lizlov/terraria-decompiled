using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200001D RID: 29
	public class DateTimeConverter : DefaultTypeConverter
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00005C24 File Offset: 0x00003E24
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			if (text == null)
			{
				return base.ConvertFromString(options, null);
			}
			if (text.Trim().Length == 0)
			{
				return DateTime.MinValue;
			}
			IFormatProvider formatProvider = ((IFormatProvider)options.CultureInfo.GetFormat(typeof(DateTimeFormatInfo))) ?? options.CultureInfo;
			DateTimeStyles dateTimeStyles = options.DateTimeStyle ?? 0;
			return string.IsNullOrEmpty(options.Format) ? DateTime.Parse(text, formatProvider, dateTimeStyles) : DateTime.ParseExact(text, options.Format, formatProvider, dateTimeStyles);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005B96 File Offset: 0x00003D96
		public DateTimeConverter()
		{
		}
	}
}
