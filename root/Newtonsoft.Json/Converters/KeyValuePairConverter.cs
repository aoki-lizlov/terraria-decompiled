using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D6 RID: 214
	public class KeyValuePairConverter : JsonConverter
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002BA40 File Offset: 0x00029C40
		private static ReflectionObject InitializeReflectionObject(Type t)
		{
			Type[] genericArguments = t.GetGenericArguments();
			Type type = genericArguments[0];
			Type type2 = genericArguments[1];
			return ReflectionObject.Create(t, t.GetConstructor(new Type[] { type, type2 }), new string[] { "Key", "Value" });
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002BA94 File Offset: 0x00029C94
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ReflectionObject reflectionObject = KeyValuePairConverter.ReflectionObjectPerType.Get(value.GetType());
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Key") : "Key");
			serializer.Serialize(writer, reflectionObject.GetValue(value, "Key"), reflectionObject.GetType("Key"));
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Value") : "Value");
			serializer.Serialize(writer, reflectionObject.GetValue(value, "Value"), reflectionObject.GetType("Value"));
			writer.WriteEndObject();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002BB3C File Offset: 0x00029D3C
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Null)
			{
				object obj = null;
				object obj2 = null;
				reader.ReadAndAssert();
				Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
				ReflectionObject reflectionObject = KeyValuePairConverter.ReflectionObjectPerType.Get(type);
				while (reader.TokenType == JsonToken.PropertyName)
				{
					string text = reader.Value.ToString();
					if (string.Equals(text, "Key", 5))
					{
						reader.ReadAndAssert();
						obj = serializer.Deserialize(reader, reflectionObject.GetType("Key"));
					}
					else if (string.Equals(text, "Value", 5))
					{
						reader.ReadAndAssert();
						obj2 = serializer.Deserialize(reader, reflectionObject.GetType("Value"));
					}
					else
					{
						reader.Skip();
					}
					reader.ReadAndAssert();
				}
				return reflectionObject.Creator(new object[] { obj, obj2 });
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to KeyValuePair.");
			}
			return null;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002BC24 File Offset: 0x00029E24
		public override bool CanConvert(Type objectType)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			return type.IsValueType() && type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(KeyValuePair);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002AB63 File Offset: 0x00028D63
		public KeyValuePairConverter()
		{
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002BC6A File Offset: 0x00029E6A
		// Note: this type is marked as 'beforefieldinit'.
		static KeyValuePairConverter()
		{
		}

		// Token: 0x040003A0 RID: 928
		private const string KeyName = "Key";

		// Token: 0x040003A1 RID: 929
		private const string ValueName = "Value";

		// Token: 0x040003A2 RID: 930
		private static readonly ThreadSafeStore<Type, ReflectionObject> ReflectionObjectPerType = new ThreadSafeStore<Type, ReflectionObject>(new Func<Type, ReflectionObject>(KeyValuePairConverter.InitializeReflectionObject));
	}
}
