using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000031 RID: 49
	public class UInt16Converter : DefaultTypeConverter
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000068D4 File Offset: 0x00004AD4
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			ushort num;
			if (ushort.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005B96 File Offset: 0x00003D96
		public UInt16Converter()
		{
		}
	}
}
