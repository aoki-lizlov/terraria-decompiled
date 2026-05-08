using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000032 RID: 50
	public class UInt32Converter : DefaultTypeConverter
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000691C File Offset: 0x00004B1C
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			uint num;
			if (uint.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005B96 File Offset: 0x00003D96
		public UInt32Converter()
		{
		}
	}
}
