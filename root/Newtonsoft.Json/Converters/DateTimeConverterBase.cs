using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D2 RID: 210
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002B154 File Offset: 0x00029354
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002AB63 File Offset: 0x00028D63
		protected DateTimeConverterBase()
		{
		}
	}
}
