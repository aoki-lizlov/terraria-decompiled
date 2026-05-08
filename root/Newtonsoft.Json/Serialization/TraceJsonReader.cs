using System;
using System.Globalization;
using System.IO;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000081 RID: 129
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00018460 File Offset: 0x00016660
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000184C1 File Offset: 0x000166C1
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000184CE File Offset: 0x000166CE
		public override bool Read()
		{
			bool flag = this._innerReader.Read();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return flag;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000184EF File Offset: 0x000166EF
		public override int? ReadAsInt32()
		{
			int? num = this._innerReader.ReadAsInt32();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return num;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00018510 File Offset: 0x00016710
		public override string ReadAsString()
		{
			string text = this._innerReader.ReadAsString();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return text;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018531 File Offset: 0x00016731
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._innerReader.ReadAsBytes();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return array;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00018552 File Offset: 0x00016752
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._innerReader.ReadAsDecimal();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return num;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00018573 File Offset: 0x00016773
		public override double? ReadAsDouble()
		{
			double? num = this._innerReader.ReadAsDouble();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return num;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00018594 File Offset: 0x00016794
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._innerReader.ReadAsBoolean();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return flag;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000185B5 File Offset: 0x000167B5
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._innerReader.ReadAsDateTime();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return dateTime;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000185D6 File Offset: 0x000167D6
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._innerReader.ReadAsDateTimeOffset();
			this._textWriter.WriteToken(this._innerReader, false, false, true);
			return dateTimeOffset;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000185F7 File Offset: 0x000167F7
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00018604 File Offset: 0x00016804
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00018611 File Offset: 0x00016811
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001861E File Offset: 0x0001681E
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001862C File Offset: 0x0001682C
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00018639 File Offset: 0x00016839
		public override object Value
		{
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00018646 File Offset: 0x00016846
		public override Type ValueType
		{
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00018653 File Offset: 0x00016853
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00018660 File Offset: 0x00016860
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00018684 File Offset: 0x00016884
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000186A8 File Offset: 0x000168A8
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x0400027A RID: 634
		private readonly JsonReader _innerReader;

		// Token: 0x0400027B RID: 635
		private readonly JsonTextWriter _textWriter;

		// Token: 0x0400027C RID: 636
		private readonly StringWriter _sw;
	}
}
