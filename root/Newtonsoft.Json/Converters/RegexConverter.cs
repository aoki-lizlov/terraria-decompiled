using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D8 RID: 216
	public class RegexConverter : JsonConverter
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002BD0C File Offset: 0x00029F0C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Regex regex = (Regex)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				this.WriteBson(bsonWriter, regex);
				return;
			}
			this.WriteJson(writer, regex, serializer);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000F188 File Offset: 0x0000D388
		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002BD3C File Offset: 0x00029F3C
		private void WriteBson(BsonWriter writer, Regex regex)
		{
			string text = null;
			if (this.HasFlag(regex.Options, 1))
			{
				text += "i";
			}
			if (this.HasFlag(regex.Options, 2))
			{
				text += "m";
			}
			if (this.HasFlag(regex.Options, 16))
			{
				text += "s";
			}
			text += "u";
			if (this.HasFlag(regex.Options, 4))
			{
				text += "x";
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002BDD4 File Offset: 0x00029FD4
		private void WriteJson(JsonWriter writer, Regex regex, JsonSerializer serializer)
		{
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Pattern") : "Pattern");
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Options") : "Options");
			serializer.Serialize(writer, regex.Options);
			writer.WriteEndObject();
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002BE50 File Offset: 0x0002A050
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadRegexObject(reader, serializer);
			}
			if (tokenType == JsonToken.String)
			{
				return this.ReadRegexString(reader);
			}
			if (tokenType != JsonToken.Null)
			{
				throw JsonSerializationException.Create(reader, "Unexpected token when reading Regex.");
			}
			return null;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002BE94 File Offset: 0x0002A094
		private object ReadRegexString(JsonReader reader)
		{
			string text = (string)reader.Value;
			int num = text.LastIndexOf('/');
			string text2 = text.Substring(1, num - 1);
			string text3 = text.Substring(num + 1);
			RegexOptions regexOptions = 0;
			string text4 = text3;
			for (int i = 0; i < text4.Length; i++)
			{
				char c = text4.get_Chars(i);
				if (c <= 'm')
				{
					if (c != 'i')
					{
						if (c == 'm')
						{
							regexOptions |= 2;
						}
					}
					else
					{
						regexOptions |= 1;
					}
				}
				else if (c != 's')
				{
					if (c == 'x')
					{
						regexOptions |= 4;
					}
				}
				else
				{
					regexOptions |= 16;
				}
			}
			return new Regex(text2, regexOptions);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002BF2C File Offset: 0x0002A12C
		private Regex ReadRegexObject(JsonReader reader, JsonSerializer serializer)
		{
			string text = null;
			RegexOptions? regexOptions = default(RegexOptions?);
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							if (text == null)
							{
								throw JsonSerializationException.Create(reader, "Error deserializing Regex. No pattern found.");
							}
							return new Regex(text, regexOptions ?? 0);
						}
					}
				}
				else
				{
					string text2 = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
					}
					if (string.Equals(text2, "Pattern", 5))
					{
						text = (string)reader.Value;
					}
					else if (string.Equals(text2, "Options", 5))
					{
						regexOptions = new RegexOptions?(serializer.Deserialize<RegexOptions>(reader));
					}
					else
					{
						reader.Skip();
					}
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002C008 File Offset: 0x0002A208
		public override bool CanConvert(Type objectType)
		{
			return objectType.Name == "Regex" && this.IsRegex(objectType);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002C025 File Offset: 0x0002A225
		[MethodImpl(8)]
		private bool IsRegex(Type objectType)
		{
			return objectType == typeof(Regex);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002AB63 File Offset: 0x00028D63
		public RegexConverter()
		{
		}

		// Token: 0x040003A3 RID: 931
		private const string PatternName = "Pattern";

		// Token: 0x040003A4 RID: 932
		private const string OptionsName = "Options";
	}
}
