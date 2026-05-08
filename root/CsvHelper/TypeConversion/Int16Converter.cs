using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000025 RID: 37
	public class Int16Converter : DefaultTypeConverter
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00005F0C File Offset: 0x0000410C
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			short num;
			if (short.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005B96 File Offset: 0x00003D96
		public Int16Converter()
		{
		}
	}
}
