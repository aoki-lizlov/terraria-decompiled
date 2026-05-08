using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000024 RID: 36
	public class GuidConverter : DefaultTypeConverter
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00005EF3 File Offset: 0x000040F3
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			if (text == null)
			{
				return base.ConvertFromString(options, text);
			}
			return new Guid(text);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005B96 File Offset: 0x00003D96
		public GuidConverter()
		{
		}
	}
}
