using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000024 RID: 36
	public class JsonTextWriter : JsonWriter
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000074CF File Offset: 0x000056CF
		private Base64Encoder Base64Encoder
		{
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000074F0 File Offset: 0x000056F0
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000074F8 File Offset: 0x000056F8
		public IArrayPool<char> ArrayPool
		{
			get
			{
				return this._arrayPool;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000750F File Offset: 0x0000570F
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00007517 File Offset: 0x00005717
		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				this._indentation = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000752F File Offset: 0x0000572F
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00007537 File Offset: 0x00005737
		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				this._quoteChar = value;
				this.UpdateCharEscapeFlags();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000755B File Offset: 0x0000575B
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00007563 File Offset: 0x00005763
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				if (value != this._indentChar)
				{
					this._indentChar = value;
					this._indentChars = null;
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000757C File Offset: 0x0000577C
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00007584 File Offset: 0x00005784
		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007590 File Offset: 0x00005790
		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000075DC File Offset: 0x000057DC
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000075E9 File Offset: 0x000057E9
		public override void Close()
		{
			base.Close();
			this.CloseBufferAndWriter();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000075F7 File Offset: 0x000057F7
		private void CloseBufferAndWriter()
		{
			if (this._writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._writeBuffer);
				this._writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				TextWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007631 File Offset: 0x00005831
		public override void WriteStartObject()
		{
			base.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			this._writer.Write('{');
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007648 File Offset: 0x00005848
		public override void WriteStartArray()
		{
			base.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			this._writer.Write('[');
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000765F File Offset: 0x0000585F
		public override void WriteStartConstructor(string name)
		{
			base.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			this._writer.Write("new ");
			this._writer.Write(name);
			this._writer.Write('(');
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007694 File Offset: 0x00005894
		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write('}');
				return;
			case JsonToken.EndArray:
				this._writer.Write(']');
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(')');
				return;
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token, null);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000076F9 File Offset: 0x000058F9
		public override void WritePropertyName(string name)
		{
			base.InternalWritePropertyName(name);
			this.WriteEscapedString(name, this._quoteName);
			this._writer.Write(':');
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000771C File Offset: 0x0000591C
		public override void WritePropertyName(string name, bool escape)
		{
			base.InternalWritePropertyName(name);
			if (escape)
			{
				this.WriteEscapedString(name, this._quoteName);
			}
			else
			{
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
				this._writer.Write(name);
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
			}
			this._writer.Write(':');
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000778D File Offset: 0x0000598D
		internal override void OnStringEscapeHandlingChanged()
		{
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007795 File Offset: 0x00005995
		private void UpdateCharEscapeFlags()
		{
			this._charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, this._quoteChar);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000077B0 File Offset: 0x000059B0
		protected override void WriteIndent()
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			this._writer.Write(this._indentChars, 0, num2 + Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				this._writer.Write(this._indentChars, num2, Math.Min(num, 12));
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007814 File Offset: 0x00005A14
		private int SetIndentChars()
		{
			string newLine = this._writer.NewLine;
			int length = newLine.Length;
			bool flag = this._indentChars != null && this._indentChars.Length == 12 + length;
			if (flag)
			{
				for (int num = 0; num != length; num++)
				{
					if (newLine.get_Chars(num) != this._indentChars[num])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				this._indentChars = (newLine + new string(this._indentChar, 12)).ToCharArray();
			}
			return length;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007894 File Offset: 0x00005A94
		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000078A3 File Offset: 0x00005AA3
		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000078B2 File Offset: 0x00005AB2
		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000078C0 File Offset: 0x00005AC0
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.WriteValueInternal(((BigInteger)value).ToString(CultureInfo.InvariantCulture), JsonToken.String);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000078FF File Offset: 0x00005AFF
		public override void WriteNull()
		{
			base.InternalWriteValue(JsonToken.Null);
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007916 File Offset: 0x00005B16
		public override void WriteUndefined()
		{
			base.InternalWriteValue(JsonToken.Undefined);
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000792D File Offset: 0x00005B2D
		public override void WriteRaw(string json)
		{
			base.InternalWriteRaw();
			this._writer.Write(json);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007941 File Offset: 0x00005B41
		public override void WriteValue(string value)
		{
			base.InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			this.WriteEscapedString(value, true);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007964 File Offset: 0x00005B64
		private void WriteEscapedString(string value, bool quote)
		{
			this.EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this._arrayPool, ref this._writeBuffer);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007997 File Offset: 0x00005B97
		public override void WriteValue(int value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000079A8 File Offset: 0x00005BA8
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000079B9 File Offset: 0x00005BB9
		public override void WriteValue(long value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000079C9 File Offset: 0x00005BC9
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value, false);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000079DA File Offset: 0x00005BDA
		public override void WriteValue(float value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000079FD File Offset: 0x00005BFD
		public override void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007A36 File Offset: 0x00005C36
		public override void WriteValue(double value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007A59 File Offset: 0x00005C59
		public override void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007A92 File Offset: 0x00005C92
		public override void WriteValue(bool value)
		{
			base.InternalWriteValue(JsonToken.Boolean);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007997 File Offset: 0x00005B97
		public override void WriteValue(short value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)value);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000079A8 File Offset: 0x00005BA8
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007AAA File Offset: 0x00005CAA
		public override void WriteValue(char value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000079A8 File Offset: 0x00005BA8
		public override void WriteValue(byte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007997 File Offset: 0x00005B97
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)value);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007AC2 File Offset: 0x00005CC2
		public override void WriteValue(decimal value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public override void WriteValue(DateTime value)
		{
			base.InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007B64 File Offset: 0x00005D64
		private int WriteValueToBuffer(DateTime value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, value, default(TimeSpan?), value.Kind, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Bytes);
			this._writer.Write(this._quoteChar);
			this.Base64Encoder.Encode(value, 0, value.Length);
			this.Base64Encoder.Flush();
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007C20 File Offset: 0x00005E20
		public override void WriteValue(DateTimeOffset value)
		{
			base.InternalWriteValue(JsonToken.Date);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private int WriteValueToBuffer(DateTimeOffset value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), 2, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007D14 File Offset: 0x00005F14
		public override void WriteValue(Guid value)
		{
			base.InternalWriteValue(JsonToken.String);
			string text = value.ToString("D", CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(text);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override void WriteValue(TimeSpan value)
		{
			base.InternalWriteValue(JsonToken.String);
			string text = value.ToString(null, CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(text);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007DBD File Offset: 0x00005FBD
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
			this.WriteEscapedString(value.OriginalString, true);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007DE4 File Offset: 0x00005FE4
		public override void WriteComment(string text)
		{
			base.InternalWriteComment();
			this._writer.Write("/*");
			this._writer.Write(text);
			this._writer.Write("*/");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007E18 File Offset: 0x00006018
		public override void WriteWhitespace(string ws)
		{
			base.InternalWriteWhitespace(ws);
			this._writer.Write(ws);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007E2D File Offset: 0x0000602D
		private void EnsureWriteBuffer()
		{
			if (this._writeBuffer == null)
			{
				this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, 35);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007E4C File Offset: 0x0000604C
		private void WriteIntegerValue(long value)
		{
			if (value >= 0L && value <= 9L)
			{
				this._writer.Write((char)(48L + value));
				return;
			}
			bool flag = value < 0L;
			this.WriteIntegerValue((ulong)(flag ? (-(ulong)value) : value), flag);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007E8C File Offset: 0x0000608C
		private void WriteIntegerValue(ulong uvalue, bool negative)
		{
			if (!negative & (uvalue <= 9UL))
			{
				this._writer.Write((char)(48UL + uvalue));
				return;
			}
			int num = this.WriteNumberToBuffer(uvalue, negative);
			this._writer.Write(this._writeBuffer, 0, num);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007ED8 File Offset: 0x000060D8
		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				this._writeBuffer[--num2] = (char)(48UL + value % 10UL);
				value /= 10UL;
			}
			while (value != 0UL);
			return num;
		}

		// Token: 0x040000BB RID: 187
		private const int IndentCharBufferSize = 12;

		// Token: 0x040000BC RID: 188
		private readonly TextWriter _writer;

		// Token: 0x040000BD RID: 189
		private Base64Encoder _base64Encoder;

		// Token: 0x040000BE RID: 190
		private char _indentChar;

		// Token: 0x040000BF RID: 191
		private int _indentation;

		// Token: 0x040000C0 RID: 192
		private char _quoteChar;

		// Token: 0x040000C1 RID: 193
		private bool _quoteName;

		// Token: 0x040000C2 RID: 194
		private bool[] _charEscapeFlags;

		// Token: 0x040000C3 RID: 195
		private char[] _writeBuffer;

		// Token: 0x040000C4 RID: 196
		private IArrayPool<char> _arrayPool;

		// Token: 0x040000C5 RID: 197
		private char[] _indentChars;
	}
}
