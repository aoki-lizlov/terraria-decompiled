using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D9 RID: 217
	public class StringEnumConverter : JsonConverter
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002C037 File Offset: 0x0002A237
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0002C03F File Offset: 0x0002A23F
		public bool CamelCaseText
		{
			[CompilerGenerated]
			get
			{
				return this.<CamelCaseText>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CamelCaseText>k__BackingField = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002C048 File Offset: 0x0002A248
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0002C050 File Offset: 0x0002A250
		public bool AllowIntegerValues
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowIntegerValues>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowIntegerValues>k__BackingField = value;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002C059 File Offset: 0x0002A259
		public StringEnumConverter()
		{
			this.AllowIntegerValues = true;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002C068 File Offset: 0x0002A268
		public StringEnumConverter(bool camelCaseText)
			: this()
		{
			this.CamelCaseText = camelCaseText;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002C078 File Offset: 0x0002A278
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string text = @enum.ToString("G");
			if (!char.IsNumber(text.get_Chars(0)) && text.get_Chars(0) != '-')
			{
				string text2 = EnumUtils.ToEnumName(@enum.GetType(), text, this.CamelCaseText);
				writer.WriteValue(text2);
				return;
			}
			if (!this.AllowIntegerValues)
			{
				throw JsonSerializationException.Create(null, writer.ContainerPath, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, text), null);
			}
			writer.WriteValue(value);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002C104 File Offset: 0x0002A304
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Null)
			{
				bool flag = ReflectionUtils.IsNullableType(objectType);
				Type type = (flag ? Nullable.GetUnderlyingType(objectType) : objectType);
				try
				{
					if (reader.TokenType == JsonToken.String)
					{
						return EnumUtils.ParseEnumName(reader.Value.ToString(), flag, !this.AllowIntegerValues, type);
					}
					if (reader.TokenType == JsonToken.Integer)
					{
						if (!this.AllowIntegerValues)
						{
							throw JsonSerializationException.Create(reader, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, reader.Value));
						}
						return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
					}
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.FormatValueForPrint(reader.Value), objectType), ex);
				}
				throw JsonSerializationException.Create(reader, "Unexpected token {0} when parsing enum.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			return null;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002C214 File Offset: 0x0002A414
		public override bool CanConvert(Type objectType)
		{
			return (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType).IsEnum();
		}

		// Token: 0x040003A5 RID: 933
		[CompilerGenerated]
		private bool <CamelCaseText>k__BackingField;

		// Token: 0x040003A6 RID: 934
		[CompilerGenerated]
		private bool <AllowIntegerValues>k__BackingField;
	}
}
