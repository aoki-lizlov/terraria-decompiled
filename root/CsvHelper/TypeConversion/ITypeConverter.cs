using System;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000028 RID: 40
	public interface ITypeConverter
	{
		// Token: 0x06000142 RID: 322
		string ConvertToString(TypeConverterOptions options, object value);

		// Token: 0x06000143 RID: 323
		object ConvertFromString(TypeConverterOptions options, string text);

		// Token: 0x06000144 RID: 324
		bool CanConvertFrom(Type type);

		// Token: 0x06000145 RID: 325
		bool CanConvertTo(Type type);
	}
}
