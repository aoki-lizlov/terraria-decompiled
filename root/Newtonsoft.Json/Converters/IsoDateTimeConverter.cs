using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000DB RID: 219
	public class IsoDateTimeConverter : DateTimeConverterBase
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002C2F6 File Offset: 0x0002A4F6
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0002C2FE File Offset: 0x0002A4FE
		public DateTimeStyles DateTimeStyles
		{
			get
			{
				return this._dateTimeStyles;
			}
			set
			{
				this._dateTimeStyles = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002C307 File Offset: 0x0002A507
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x0002C318 File Offset: 0x0002A518
		public string DateTimeFormat
		{
			get
			{
				return this._dateTimeFormat ?? string.Empty;
			}
			set
			{
				this._dateTimeFormat = (string.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002C32C File Offset: 0x0002A52C
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0002C33D File Offset: 0x0002A53D
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.CurrentCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002C348 File Offset: 0x0002A548
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			string text;
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if ((this._dateTimeStyles & 16) == 16 || (this._dateTimeStyles & 64) == 64)
				{
					dateTime = dateTime.ToUniversalTime();
				}
				text = dateTime.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException("Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {0}.".FormatWith(CultureInfo.InvariantCulture, ReflectionUtils.GetObjectType(value)));
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				if ((this._dateTimeStyles & 16) == 16 || (this._dateTimeStyles & 64) == 64)
				{
					dateTimeOffset = dateTimeOffset.ToUniversalTime();
				}
				text = dateTimeOffset.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			writer.WriteValue(text);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002C418 File Offset: 0x0002A618
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			Type type = (flag ? Nullable.GetUnderlyingType(objectType) : objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullableType(objectType))
				{
					throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else if (reader.TokenType == JsonToken.Date)
			{
				if (type == typeof(DateTimeOffset))
				{
					if (!(reader.Value is DateTimeOffset))
					{
						return new DateTimeOffset((DateTime)reader.Value);
					}
					return reader.Value;
				}
				else
				{
					if (reader.Value is DateTimeOffset)
					{
						return ((DateTimeOffset)reader.Value).DateTime;
					}
					return reader.Value;
				}
			}
			else
			{
				if (reader.TokenType != JsonToken.String)
				{
					throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
				}
				string text = reader.Value.ToString();
				if (string.IsNullOrEmpty(text) && flag)
				{
					return null;
				}
				if (type == typeof(DateTimeOffset))
				{
					if (!string.IsNullOrEmpty(this._dateTimeFormat))
					{
						return DateTimeOffset.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
					}
					return DateTimeOffset.Parse(text, this.Culture, this._dateTimeStyles);
				}
				else
				{
					if (!string.IsNullOrEmpty(this._dateTimeFormat))
					{
						return DateTime.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
					}
					return DateTime.Parse(text, this.Culture, this._dateTimeStyles);
				}
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002C5B1 File Offset: 0x0002A7B1
		public IsoDateTimeConverter()
		{
		}

		// Token: 0x040003A7 RID: 935
		private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x040003A8 RID: 936
		private DateTimeStyles _dateTimeStyles = 128;

		// Token: 0x040003A9 RID: 937
		private string _dateTimeFormat;

		// Token: 0x040003AA RID: 938
		private CultureInfo _culture;
	}
}
