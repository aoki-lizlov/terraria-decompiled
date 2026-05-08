using System;
using System.Globalization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D7 RID: 215
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectIdConverter : JsonConverter
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0002BC84 File Offset: 0x00029E84
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			BsonObjectId bsonObjectId = (BsonObjectId)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				bsonWriter.WriteObjectId(bsonObjectId.Value);
				return;
			}
			writer.WriteValue(bsonObjectId.Value);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002BCBB File Offset: 0x00029EBB
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Bytes)
			{
				throw new JsonSerializationException("Expected Bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			return new BsonObjectId((byte[])reader.Value);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002BCF7 File Offset: 0x00029EF7
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BsonObjectId);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002AB63 File Offset: 0x00028D63
		public BsonObjectIdConverter()
		{
		}
	}
}
