using System;
using System.Globalization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000033 RID: 51
	public class UInt64Converter : DefaultTypeConverter
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00006964 File Offset: 0x00004B64
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			NumberStyles numberStyles = options.NumberStyle ?? 7;
			ulong num;
			if (ulong.TryParse(text, numberStyles, options.CultureInfo, ref num))
			{
				return num;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005B96 File Offset: 0x00003D96
		public UInt64Converter()
		{
		}
	}
}
