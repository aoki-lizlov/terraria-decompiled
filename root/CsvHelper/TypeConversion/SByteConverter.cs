using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200002A RID: 42
	public class SByteConverter : DefaultTypeConverter
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00006090 File Offset: 0x00004290
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			sbyte b;
			if (sbyte.TryParse(text, numberStyles, options.CultureInfo, ref b))
			{
				return b;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005B96 File Offset: 0x00003D96
		public SByteConverter()
		{
		}
	}
}
