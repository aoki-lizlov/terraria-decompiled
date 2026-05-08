using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000029 RID: 41
	public abstract class JsonReader : IDisposable
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008030 File Offset: 0x00006230
		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008038 File Offset: 0x00006238
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00008040 File Offset: 0x00006240
		public bool CloseInput
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseInput>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CloseInput>k__BackingField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008049 File Offset: 0x00006249
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00008051 File Offset: 0x00006251
		public bool SupportMultipleContent
		{
			[CompilerGenerated]
			get
			{
				return this.<SupportMultipleContent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SupportMultipleContent>k__BackingField = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000805A File Offset: 0x0000625A
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00008062 File Offset: 0x00006262
		public virtual char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			protected internal set
			{
				this._quoteChar = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000806B File Offset: 0x0000626B
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00008073 File Offset: 0x00006273
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000808F File Offset: 0x0000628F
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00008097 File Offset: 0x00006297
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling;
			}
			set
			{
				if (value < DateParseHandling.None || value > DateParseHandling.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateParseHandling = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000080B3 File Offset: 0x000062B3
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000080BB File Offset: 0x000062BB
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling;
			}
			set
			{
				if (value < FloatParseHandling.Double || value > FloatParseHandling.Decimal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatParseHandling = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000080D7 File Offset: 0x000062D7
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000080DF File Offset: 0x000062DF
		public string DateFormatString
		{
			get
			{
				return this._dateFormatString;
			}
			set
			{
				this._dateFormatString = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000080E8 File Offset: 0x000062E8
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000080F0 File Offset: 0x000062F0
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000812E File Offset: 0x0000632E
		public virtual JsonToken TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008136 File Offset: 0x00006336
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000813E File Offset: 0x0000633E
		public virtual Type ValueType
		{
			get
			{
				object value = this._value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008154 File Offset: 0x00006354
		public virtual int Depth
		{
			get
			{
				int num = ((this._stack != null) ? this._stack.Count : 0);
				if (JsonTokenUtils.IsStartToken(this.TokenType) || this._currentPosition.Type == JsonContainerType.None)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008198 File Offset: 0x00006398
		public virtual string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? jsonPosition = ((this._currentState != JsonReader.State.ArrayStart && this._currentState != JsonReader.State.ConstructorStart && this._currentState != JsonReader.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : default(JsonPosition?));
				return JsonPosition.BuildPath(this._stack, jsonPosition);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000081FF File Offset: 0x000063FF
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00008210 File Offset: 0x00006410
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008219 File Offset: 0x00006419
		internal JsonPosition GetPosition(int depth)
		{
			if (this._stack != null && depth < this._stack.Count)
			{
				return this._stack[depth];
			}
			return this._currentPosition;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008244 File Offset: 0x00006444
		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this._dateParseHandling = DateParseHandling.DateTime;
			this._floatParseHandling = FloatParseHandling.Double;
			this.CloseInput = true;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008270 File Offset: 0x00006470
		private void Push(JsonContainerType value)
		{
			this.UpdateScopeWithFinishedValue();
			if (this._currentPosition.Type == JsonContainerType.None)
			{
				this._currentPosition = new JsonPosition(value);
				return;
			}
			if (this._stack == null)
			{
				this._stack = new List<JsonPosition>();
			}
			this._stack.Add(this._currentPosition);
			this._currentPosition = new JsonPosition(value);
			if (this._maxDepth != null && this.Depth + 1 > this._maxDepth && !this._hasExceededMaxDepth)
			{
				this._hasExceededMaxDepth = true;
				throw JsonReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, this._maxDepth));
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008330 File Offset: 0x00006530
		private JsonContainerType Pop()
		{
			JsonPosition jsonPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				jsonPosition = this._currentPosition;
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				jsonPosition = this._currentPosition;
				this._currentPosition = default(JsonPosition);
			}
			if (this._maxDepth != null && this.Depth <= this._maxDepth)
			{
				this._hasExceededMaxDepth = false;
			}
			return jsonPosition.Type;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000083E1 File Offset: 0x000065E1
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x060001A1 RID: 417
		public abstract bool Read();

		// Token: 0x060001A2 RID: 418 RVA: 0x000083F0 File Offset: 0x000065F0
		public virtual int? ReadAsInt32()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
					if (!(this.Value is int))
					{
						this.SetToken(JsonToken.Integer, Convert.ToInt32(this.Value, CultureInfo.InvariantCulture), false);
					}
					return new int?((int)this.Value);
				case JsonToken.String:
				{
					string text = (string)this.Value;
					return this.ReadInt32String(text);
				}
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_0034;
				}
				throw JsonReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_0034:
			return default(int?);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000084A8 File Offset: 0x000066A8
		internal int? ReadInt32String(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(int?);
			}
			int num;
			if (int.TryParse(s, 7, this.Culture, ref num))
			{
				this.SetToken(JsonToken.Integer, num, false);
				return new int?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008518 File Offset: 0x00006718
		public virtual string ReadAsString()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_002E;
					}
					return (string)this.Value;
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				goto IL_002E;
			}
			return null;
			IL_002E:
			if (JsonTokenUtils.IsPrimitiveToken(contentToken) && this.Value != null)
			{
				IFormattable formattable = this.Value as IFormattable;
				string text;
				if (formattable != null)
				{
					text = formattable.ToString(null, this.Culture);
				}
				else
				{
					Uri uri = this.Value as Uri;
					text = ((uri != null) ? uri.OriginalString : this.Value.ToString());
				}
				this.SetToken(JsonToken.String, text, false);
				return text;
			}
			throw JsonReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000085D4 File Offset: 0x000067D4
		public virtual byte[] ReadAsBytes()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				switch (contentToken)
				{
				case JsonToken.None:
					break;
				case JsonToken.StartObject:
				{
					this.ReadIntoWrappedTypeObject();
					byte[] array = this.ReadAsBytes();
					this.ReaderReadAndAssert();
					if (this.TokenType != JsonToken.EndObject)
					{
						throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
					this.SetToken(JsonToken.Bytes, array, false);
					return array;
				}
				case JsonToken.StartArray:
					return this.ReadArrayIntoByteArray();
				default:
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_0122;
					}
					string text = (string)this.Value;
					byte[] array2;
					Guid guid;
					if (text.Length == 0)
					{
						array2 = CollectionUtils.ArrayEmpty<byte>();
					}
					else if (ConvertUtils.TryConvertGuid(text, out guid))
					{
						array2 = guid.ToByteArray();
					}
					else
					{
						array2 = Convert.FromBase64String(text);
					}
					this.SetToken(JsonToken.Bytes, array2, false);
					return array2;
				}
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Bytes)
				{
					goto IL_0122;
				}
				if (this.ValueType == typeof(Guid))
				{
					byte[] array3 = ((Guid)this.Value).ToByteArray();
					this.SetToken(JsonToken.Bytes, array3, false);
					return array3;
				}
				return (byte[])this.Value;
			}
			return null;
			IL_0122:
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008720 File Offset: 0x00006920
		internal byte[] ReadArrayIntoByteArray()
		{
			List<byte> list = new List<byte>();
			do
			{
				if (!this.Read())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008760 File Offset: 0x00006960
		private bool ReadArrayElementIntoByteArrayReportDone(List<byte> buffer)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType <= JsonToken.Comment)
			{
				if (tokenType == JsonToken.None)
				{
					throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
				}
				if (tokenType == JsonToken.Comment)
				{
					return false;
				}
			}
			else
			{
				if (tokenType == JsonToken.Integer)
				{
					buffer.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
					return false;
				}
				if (tokenType == JsonToken.EndArray)
				{
					return true;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000087D4 File Offset: 0x000069D4
		public virtual double? ReadAsDouble()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
					if (!(this.Value is double))
					{
						double num;
						if (this.Value is BigInteger)
						{
							num = (double)((BigInteger)this.Value);
						}
						else
						{
							num = Convert.ToDouble(this.Value, CultureInfo.InvariantCulture);
						}
						this.SetToken(JsonToken.Float, num, false);
					}
					return new double?((double)this.Value);
				case JsonToken.String:
					return this.ReadDoubleString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_0034;
				}
				throw JsonReaderException.Create(this, "Error reading double. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_0034:
			return default(double?);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000088AC File Offset: 0x00006AAC
		internal double? ReadDoubleString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(double?);
			}
			double num;
			if (double.TryParse(s, 231, this.Culture, ref num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new double?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to double: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008920 File Offset: 0x00006B20
		public virtual bool? ReadAsBoolean()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					bool flag;
					if (this.Value is BigInteger)
					{
						flag = (BigInteger)this.Value != 0L;
					}
					else
					{
						flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
					}
					this.SetToken(JsonToken.Boolean, flag, false);
					return new bool?(flag);
				}
				case JsonToken.String:
					return this.ReadBooleanString((string)this.Value);
				case JsonToken.Boolean:
					return new bool?((bool)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_0034;
				}
				throw JsonReaderException.Create(this, "Error reading boolean. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_0034:
			return default(bool?);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000089F4 File Offset: 0x00006BF4
		internal bool? ReadBooleanString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(bool?);
			}
			bool flag;
			if (bool.TryParse(s, ref flag))
			{
				this.SetToken(JsonToken.Boolean, flag, false);
				return new bool?(flag);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to boolean: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008A60 File Offset: 0x00006C60
		public virtual decimal? ReadAsDecimal()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
					if (!(this.Value is decimal))
					{
						this.SetToken(JsonToken.Float, Convert.ToDecimal(this.Value, CultureInfo.InvariantCulture), false);
					}
					return new decimal?((decimal)this.Value);
				case JsonToken.String:
					return this.ReadDecimalString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_0034;
				}
				throw JsonReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_0034:
			return default(decimal?);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008B14 File Offset: 0x00006D14
		internal decimal? ReadDecimalString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(decimal?);
			}
			decimal num;
			if (decimal.TryParse(s, 111, this.Culture, ref num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008B84 File Offset: 0x00006D84
		public virtual DateTime? ReadAsDateTime()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_0084;
					}
					string text = (string)this.Value;
					return this.ReadDateTimeString(text);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_0084;
				}
				if (this.Value is DateTimeOffset)
				{
					this.SetToken(JsonToken.Date, ((DateTimeOffset)this.Value).DateTime, false);
				}
				return new DateTime?((DateTime)this.Value);
			}
			return default(DateTime?);
			IL_0084:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008C38 File Offset: 0x00006E38
		internal DateTime? ReadDateTimeString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(DateTime?);
			}
			DateTime dateTime;
			if (DateTimeUtils.TryParseDateTime(s, this.DateTimeZoneHandling, this._dateFormatString, this.Culture, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			if (DateTime.TryParse(s, this.Culture, 128, ref dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			throw JsonReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008CF0 File Offset: 0x00006EF0
		public virtual DateTimeOffset? ReadAsDateTimeOffset()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_0081;
					}
					string text = (string)this.Value;
					return this.ReadDateTimeOffsetString(text);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_0081;
				}
				if (this.Value is DateTime)
				{
					this.SetToken(JsonToken.Date, new DateTimeOffset((DateTime)this.Value), false);
				}
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			return default(DateTimeOffset?);
			IL_0081:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008D9C File Offset: 0x00006F9C
		internal DateTimeOffset? ReadDateTimeOffsetString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(DateTimeOffset?);
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeUtils.TryParseDateTimeOffset(s, this._dateFormatString, this.Culture, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			if (DateTimeOffset.TryParse(s, this.Culture, 128, ref dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to DateTimeOffset: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008E3C File Offset: 0x0000703C
		internal void ReaderReadAndAssert()
		{
			if (!this.Read())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008E4D File Offset: 0x0000704D
		internal JsonReaderException CreateUnexpectedEndException()
		{
			return JsonReaderException.Create(this, "Unexpected end when reading JSON.");
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008E5C File Offset: 0x0000705C
		internal void ReadIntoWrappedTypeObject()
		{
			this.ReaderReadAndAssert();
			if (this.Value != null && this.Value.ToString() == "$type")
			{
				this.ReaderReadAndAssert();
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]", 4))
				{
					this.ReaderReadAndAssert();
					if (this.Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008EF0 File Offset: 0x000070F0
		public void Skip()
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				this.Read();
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008F32 File Offset: 0x00007132
		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null, true);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008F3D File Offset: 0x0000713D
		protected void SetToken(JsonToken newToken, object value)
		{
			this.SetToken(newToken, value, true);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008F48 File Offset: 0x00007148
		protected void SetToken(JsonToken newToken, object value, bool updateIndex)
		{
			this._tokenType = newToken;
			this._value = value;
			switch (newToken)
			{
			case JsonToken.StartObject:
				this._currentState = JsonReader.State.ObjectStart;
				this.Push(JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this._currentState = JsonReader.State.ArrayStart;
				this.Push(JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this._currentState = JsonReader.State.ConstructorStart;
				this.Push(JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				this._currentState = JsonReader.State.Property;
				this._currentPosition.PropertyName = (string)value;
				return;
			case JsonToken.Comment:
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.SetPostValueState(updateIndex);
				break;
			case JsonToken.EndObject:
				this.ValidateEnd(JsonToken.EndObject);
				return;
			case JsonToken.EndArray:
				this.ValidateEnd(JsonToken.EndArray);
				return;
			case JsonToken.EndConstructor:
				this.ValidateEnd(JsonToken.EndConstructor);
				return;
			default:
				return;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009019 File Offset: 0x00007219
		internal void SetPostValueState(bool updateIndex)
		{
			if (this.Peek() != JsonContainerType.None)
			{
				this._currentState = JsonReader.State.PostValue;
			}
			else
			{
				this.SetFinished();
			}
			if (updateIndex)
			{
				this.UpdateScopeWithFinishedValue();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000903B File Offset: 0x0000723B
		private void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000905C File Offset: 0x0000725C
		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, "JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (this.Peek() != JsonContainerType.None)
			{
				this._currentState = JsonReader.State.PostValue;
				return;
			}
			this.SetFinished();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000090B4 File Offset: 0x000072B4
		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this.SetFinished();
				return;
			case JsonContainerType.Object:
				this._currentState = JsonReader.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonReader.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonReader.State.Constructor;
				return;
			default:
				throw JsonReaderException.Create(this, "While setting the reader state back to current object an unexpected JsonType was encountered: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000911B File Offset: 0x0000731B
		private void SetFinished()
		{
			if (this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.Start;
				return;
			}
			this._currentState = JsonReader.State.Finished;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009135 File Offset: 0x00007335
		private JsonContainerType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JsonContainerType.Object;
			case JsonToken.EndArray:
				return JsonContainerType.Array;
			case JsonToken.EndConstructor:
				return JsonContainerType.Constructor;
			default:
				throw JsonReaderException.Create(this, "Not a valid close JsonToken: {0}".FormatWith(CultureInfo.InvariantCulture, token));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000916F File Offset: 0x0000736F
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000917E File Offset: 0x0000737E
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009196 File Offset: 0x00007396
		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._tokenType = JsonToken.None;
			this._value = null;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000091AD File Offset: 0x000073AD
		internal void ReadAndAssert()
		{
			if (!this.Read())
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000091C3 File Offset: 0x000073C3
		internal bool ReadAndMoveToContent()
		{
			return this.Read() && this.MoveToContent();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000091D8 File Offset: 0x000073D8
		internal bool MoveToContent()
		{
			JsonToken jsonToken = this.TokenType;
			while (jsonToken == JsonToken.None || jsonToken == JsonToken.Comment)
			{
				if (!this.Read())
				{
					return false;
				}
				jsonToken = this.TokenType;
			}
			return true;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009208 File Offset: 0x00007408
		private JsonToken GetContentToken()
		{
			while (this.Read())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					return tokenType;
				}
			}
			this.SetToken(JsonToken.None);
			return JsonToken.None;
		}

		// Token: 0x040000CA RID: 202
		private JsonToken _tokenType;

		// Token: 0x040000CB RID: 203
		private object _value;

		// Token: 0x040000CC RID: 204
		internal char _quoteChar;

		// Token: 0x040000CD RID: 205
		internal JsonReader.State _currentState;

		// Token: 0x040000CE RID: 206
		private JsonPosition _currentPosition;

		// Token: 0x040000CF RID: 207
		private CultureInfo _culture;

		// Token: 0x040000D0 RID: 208
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x040000D1 RID: 209
		private int? _maxDepth;

		// Token: 0x040000D2 RID: 210
		private bool _hasExceededMaxDepth;

		// Token: 0x040000D3 RID: 211
		internal DateParseHandling _dateParseHandling;

		// Token: 0x040000D4 RID: 212
		internal FloatParseHandling _floatParseHandling;

		// Token: 0x040000D5 RID: 213
		private string _dateFormatString;

		// Token: 0x040000D6 RID: 214
		private List<JsonPosition> _stack;

		// Token: 0x040000D7 RID: 215
		[CompilerGenerated]
		private bool <CloseInput>k__BackingField;

		// Token: 0x040000D8 RID: 216
		[CompilerGenerated]
		private bool <SupportMultipleContent>k__BackingField;

		// Token: 0x02000106 RID: 262
		protected internal enum State
		{
			// Token: 0x04000420 RID: 1056
			Start,
			// Token: 0x04000421 RID: 1057
			Complete,
			// Token: 0x04000422 RID: 1058
			Property,
			// Token: 0x04000423 RID: 1059
			ObjectStart,
			// Token: 0x04000424 RID: 1060
			Object,
			// Token: 0x04000425 RID: 1061
			ArrayStart,
			// Token: 0x04000426 RID: 1062
			Array,
			// Token: 0x04000427 RID: 1063
			Closed,
			// Token: 0x04000428 RID: 1064
			PostValue,
			// Token: 0x04000429 RID: 1065
			ConstructorStart,
			// Token: 0x0400042A RID: 1066
			Constructor,
			// Token: 0x0400042B RID: 1067
			Error,
			// Token: 0x0400042C RID: 1068
			Finished
		}
	}
}
