using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200001A RID: 26
	public class ByteConverter : DefaultTypeConverter
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			byte b;
			if (byte.TryParse(text, numberStyles, options.CultureInfo, ref b))
			{
				return b;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005B96 File Offset: 0x00003D96
		public ByteConverter()
		{
		}
	}
}
