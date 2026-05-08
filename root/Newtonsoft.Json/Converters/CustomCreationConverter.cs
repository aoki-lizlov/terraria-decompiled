using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D1 RID: 209
	public abstract class CustomCreationConverter<T> : JsonConverter
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002B0EB File Offset: 0x000292EB
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002B0F8 File Offset: 0x000292F8
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x06000AAE RID: 2734
		public abstract T Create(Type objectType);

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002B140 File Offset: 0x00029340
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002AB63 File Offset: 0x00028D63
		protected CustomCreationConverter()
		{
		}
	}
}
