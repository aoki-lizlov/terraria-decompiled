using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000021 RID: 33
	public class DoubleConverter : DefaultTypeConverter
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00005DF0 File Offset: 0x00003FF0
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 167;
			double num;
			if (double.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005B96 File Offset: 0x00003D96
		public DoubleConverter()
		{
		}
	}
}
