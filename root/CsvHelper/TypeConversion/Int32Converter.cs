using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000026 RID: 38
	public class Int32Converter : DefaultTypeConverter
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00005F54 File Offset: 0x00004154
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			int num;
			if (int.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005B96 File Offset: 0x00003D96
		public Int32Converter()
		{
		}
	}
}
