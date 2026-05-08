using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000020 RID: 32
	public class DefaultTypeConverter : ITypeConverter
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public virtual string ConvertToString(TypeConverterOptions options, object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(options.Format, options.CultureInfo);
			}
			return value.ToString();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005DE1 File Offset: 0x00003FE1
		public virtual object ConvertFromString(TypeConverterOptions options, string text)
		{
			throw new CsvTypeConverterException("The conversion cannot be performed.");
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005DED File Offset: 0x00003FED
		public virtual bool CanConvertFrom(Type type)
		{
			return false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005B84 File Offset: 0x00003D84
		public virtual bool CanConvertTo(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002253 File Offset: 0x00000453
		public DefaultTypeConverter()
		{
		}
	}
}
