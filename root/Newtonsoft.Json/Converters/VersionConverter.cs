using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000DA RID: 218
	public class VersionConverter : JsonConverter
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002C22C File Offset: 0x0002A42C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			if (value is Version)
			{
				writer.WriteValue(value.ToString());
				return;
			}
			throw new JsonSerializationException("Expected Version object value");
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002C258 File Offset: 0x0002A458
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (reader.TokenType == JsonToken.String)
			{
				try
				{
					return new Version((string)reader.Value);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error parsing version string: {0}".FormatWith(CultureInfo.InvariantCulture, reader.Value), ex);
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected token or value when parsing version. Token: {0}, Value: {1}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002C2E4 File Offset: 0x0002A4E4
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Version);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002AB63 File Offset: 0x00028D63
		public VersionConverter()
		{
		}
	}
}
