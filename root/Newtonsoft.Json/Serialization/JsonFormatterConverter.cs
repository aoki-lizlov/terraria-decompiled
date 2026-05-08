using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000084 RID: 132
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x00019171 File Offset: 0x00017371
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000191A4 File Offset: 0x000173A4
		private T GetTokenValue<T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)global::System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000191D8 File Offset: 0x000173D8
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._reader.CreateISerializableItem(jtoken, type, this._contract, this._member);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00019223 File Offset: 0x00017423
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value is JValue)
			{
				value = ((JValue)value).Value;
			}
			return global::System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00019251 File Offset: 0x00017451
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001925A File Offset: 0x0001745A
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00019263 File Offset: 0x00017463
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001926C File Offset: 0x0001746C
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019275 File Offset: 0x00017475
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001927E File Offset: 0x0001747E
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00019287 File Offset: 0x00017487
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019290 File Offset: 0x00017490
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00019299 File Offset: 0x00017499
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000192A2 File Offset: 0x000174A2
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000192AB File Offset: 0x000174AB
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000192B4 File Offset: 0x000174B4
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000192BD File Offset: 0x000174BD
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000192C6 File Offset: 0x000174C6
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000192CF File Offset: 0x000174CF
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04000284 RID: 644
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x04000285 RID: 645
		private readonly JsonISerializableContract _contract;

		// Token: 0x04000286 RID: 646
		private readonly JsonProperty _member;
	}
}
