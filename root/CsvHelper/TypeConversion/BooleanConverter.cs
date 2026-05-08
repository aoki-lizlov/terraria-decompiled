using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000019 RID: 25
	public class BooleanConverter : DefaultTypeConverter
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00005A70 File Offset: 0x00003C70
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			bool flag;
			if (bool.TryParse(text, ref flag))
			{
				return flag;
			}
			short num;
			if (short.TryParse(text, ref num))
			{
				if (num == 0)
				{
					return false;
				}
				if (num == 1)
				{
					return true;
				}
			}
			string text2 = (text ?? string.Empty).Trim();
			foreach (string text3 in options.BooleanTrueValues)
			{
				if (options.CultureInfo.CompareInfo.Compare(text3, text2, 1) == 0)
				{
					return true;
				}
			}
			foreach (string text4 in options.BooleanFalseValues)
			{
				if (options.CultureInfo.CompareInfo.Compare(text4, text2, 1) == 0)
				{
					return false;
				}
			}
			return base.ConvertFromString(options, text);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005B96 File Offset: 0x00003D96
		public BooleanConverter()
		{
		}
	}
}
