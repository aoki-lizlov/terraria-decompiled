using System;
using System.Reflection;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000022 RID: 34
	public class EnumConverter : DefaultTypeConverter
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00005E3C File Offset: 0x0000403C
		public EnumConverter(Type type)
		{
			typeof(Enum).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
			if (!typeof(Enum).IsAssignableFrom(type))
			{
				throw new ArgumentException(string.Format("'{0}' is not an Enum.", type.FullName));
			}
			this.type = type;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005E9C File Offset: 0x0000409C
		public override object ConvertFromString(TypeConverterOptions options, string text)
		{
			object obj;
			try
			{
				obj = Enum.Parse(this.type, text, true);
			}
			catch
			{
				obj = base.ConvertFromString(options, text);
			}
			return obj;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005B84 File Offset: 0x00003D84
		public override bool CanConvertFrom(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0400002B RID: 43
		private readonly Type type;
	}
}
