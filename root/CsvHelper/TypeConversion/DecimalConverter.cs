using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200001F RID: 31
	public class DecimalConverter : DefaultTypeConverter
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00005D5C File Offset: 0x00003F5C
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 167;
			decimal num;
			if (decimal.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005B96 File Offset: 0x00003D96
		public DecimalConverter()
		{
		}
	}
}
