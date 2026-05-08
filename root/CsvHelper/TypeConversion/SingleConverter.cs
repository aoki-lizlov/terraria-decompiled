using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200002B RID: 43
	public class SingleConverter : DefaultTypeConverter
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000060D8 File Offset: 0x000042D8
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 167;
			float num;
			if (float.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005B96 File Offset: 0x00003D96
		public SingleConverter()
		{
		}
	}
}
