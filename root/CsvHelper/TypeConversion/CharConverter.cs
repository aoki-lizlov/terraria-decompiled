using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200001B RID: 27
	public class CharConverter : DefaultTypeConverter
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			if (text != null && text.Length > 1)
			{
				text = text.Trim();
			}
			char c;
			if (char.TryParse(text, ref c))
			{
				return c;
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005B96 File Offset: 0x00003D96
		public CharConverter()
		{
		}
	}
}
