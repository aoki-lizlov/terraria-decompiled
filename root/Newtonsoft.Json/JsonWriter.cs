using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000032 RID: 50
	public abstract class JsonWriter : IDisposable
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		internal static JsonWriter.State[][] BuildStateArray()
		{
			List<JsonWriter.State[]> list = Enumerable.ToList<JsonWriter.State[]>(JsonWriter.StateArrayTempate);
			JsonWriter.State[] array = JsonWriter.StateArrayTempate[0];
			JsonWriter.State[] array2 = JsonWriter.StateArrayTempate[7];
			foreach (object obj in EnumUtils.GetValues(typeof(JsonToken)))
			{
				JsonToken jsonToken = (JsonToken)obj;
				if (list.Count <= (int)jsonToken)
				{
					switch (jsonToken)
					{
					case JsonToken.Integer:
					case JsonToken.Float:
					case JsonToken.String:
					case JsonToken.Boolean:
					case JsonToken.Null:
					case JsonToken.Undefined:
					case JsonToken.Date:
					case JsonToken.Bytes:
						list.Add(array2);
						continue;
					}
					list.Add(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000AD7C File Offset: 0x00008F7C
		static JsonWriter()
		{
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000AF4F File Offset: 0x0000914F
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000AF57 File Offset: 0x00009157
		public bool CloseOutput
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseOutput>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CloseOutput>k__BackingField = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000AF60 File Offset: 0x00009160
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000AF68 File Offset: 0x00009168
		public bool AutoCompleteOnClose
		{
			[CompilerGenerated]
			get
			{
				return this.<AutoCompleteOnClose>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AutoCompleteOnClose>k__BackingField = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000AF74 File Offset: 0x00009174
		protected internal int Top
		{
			get
			{
				int num = ((this._stack != null) ? this._stack.Count : 0);
				if (this.Peek() != JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				default:
					throw JsonWriterException.Create(this, "Invalid state: " + this._currentState, null);
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B018 File Offset: 0x00009218
		internal string ContainerPath
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None || this._stack == null)
				{
					return string.Empty;
				}
				return JsonPosition.BuildPath(this._stack, default(JsonPosition?));
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000B054 File Offset: 0x00009254
		public string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? jsonPosition = ((this._currentState != JsonWriter.State.ArrayStart && this._currentState != JsonWriter.State.ConstructorStart && this._currentState != JsonWriter.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : default(JsonPosition?));
				return JsonPosition.BuildPath(this._stack, jsonPosition);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B0BA File Offset: 0x000092BA
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000B0C2 File Offset: 0x000092C2
		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				if (value < Formatting.None || value > Formatting.Indented)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._formatting = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B0DE File Offset: 0x000092DE
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000B0E6 File Offset: 0x000092E6
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling;
			}
			set
			{
				if (value < DateFormatHandling.IsoDateFormat || value > DateFormatHandling.MicrosoftDateFormat)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateFormatHandling = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B102 File Offset: 0x00009302
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000B10A File Offset: 0x0000930A
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B126 File Offset: 0x00009326
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000B12E File Offset: 0x0000932E
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling;
			}
			set
			{
				if (value < StringEscapeHandling.Default || value > StringEscapeHandling.EscapeHtml)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._stringEscapeHandling = value;
				this.OnStringEscapeHandlingChanged();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00002C0D File Offset: 0x00000E0D
		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000B150 File Offset: 0x00009350
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000B158 File Offset: 0x00009358
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling;
			}
			set
			{
				if (value < FloatFormatHandling.String || value > FloatFormatHandling.DefaultValue)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatFormatHandling = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B174 File Offset: 0x00009374
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000B17C File Offset: 0x0000937C
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B185 File Offset: 0x00009385
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000B196 File Offset: 0x00009396
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

		// Token: 0x06000283 RID: 643 RVA: 0x0000B19F File Offset: 0x0000939F
		protected JsonWriter()
		{
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this.CloseOutput = true;
			this.AutoCompleteOnClose = true;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B1CA File Offset: 0x000093CA
		internal void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B1E9 File Offset: 0x000093E9
		private void Push(JsonContainerType value)
		{
			if (this._currentPosition.Type != JsonContainerType.None)
			{
				if (this._stack == null)
				{
					this._stack = new List<JsonPosition>();
				}
				this._stack.Add(this._currentPosition);
			}
			this._currentPosition = new JsonPosition(value);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B228 File Offset: 0x00009428
		private JsonContainerType Pop()
		{
			ref JsonPosition currentPosition = this._currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				this._currentPosition = default(JsonPosition);
			}
			return currentPosition.Type;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B29A File Offset: 0x0000949A
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x06000288 RID: 648
		public abstract void Flush();

		// Token: 0x06000289 RID: 649 RVA: 0x0000B2A7 File Offset: 0x000094A7
		public virtual void Close()
		{
			if (this.AutoCompleteOnClose)
			{
				this.AutoCompleteAll();
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B2B7 File Offset: 0x000094B7
		public virtual void WriteStartObject()
		{
			this.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B2C1 File Offset: 0x000094C1
		public virtual void WriteEndObject()
		{
			this.InternalWriteEnd(JsonContainerType.Object);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B2CA File Offset: 0x000094CA
		public virtual void WriteStartArray()
		{
			this.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B2D4 File Offset: 0x000094D4
		public virtual void WriteEndArray()
		{
			this.InternalWriteEnd(JsonContainerType.Array);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B2DD File Offset: 0x000094DD
		public virtual void WriteStartConstructor(string name)
		{
			this.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B2E7 File Offset: 0x000094E7
		public virtual void WriteEndConstructor()
		{
			this.InternalWriteEnd(JsonContainerType.Constructor);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public virtual void WritePropertyName(string name)
		{
			this.InternalWritePropertyName(name);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B2F9 File Offset: 0x000094F9
		public virtual void WritePropertyName(string name, bool escape)
		{
			this.WritePropertyName(name);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B302 File Offset: 0x00009502
		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B310 File Offset: 0x00009510
		public void WriteToken(JsonReader reader)
		{
			this.WriteToken(reader, true);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B31A File Offset: 0x0000951A
		public void WriteToken(JsonReader reader, bool writeChildren)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this.WriteToken(reader, writeChildren, true, true);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B334 File Offset: 0x00009534
		public void WriteToken(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
				this.WriteStartObject();
				return;
			case JsonToken.StartArray:
				this.WriteStartArray();
				return;
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteStartConstructor(value.ToString());
				return;
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WritePropertyName(value.ToString());
				return;
			case JsonToken.Comment:
				this.WriteComment((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Raw:
				this.WriteRawValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is BigInteger)
				{
					this.WriteValue((BigInteger)value);
					return;
				}
				this.WriteValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal)
				{
					this.WriteValue((decimal)value);
					return;
				}
				if (value is double)
				{
					this.WriteValue((double)value);
					return;
				}
				if (value is float)
				{
					this.WriteValue((float)value);
					return;
				}
				this.WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.String:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(value.ToString());
				return;
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Null:
				this.WriteNull();
				return;
			case JsonToken.Undefined:
				this.WriteUndefined();
				return;
			case JsonToken.EndObject:
				this.WriteEndObject();
				return;
			case JsonToken.EndArray:
				this.WriteEndArray();
				return;
			case JsonToken.EndConstructor:
				this.WriteEndConstructor();
				return;
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is DateTimeOffset)
				{
					this.WriteValue((DateTimeOffset)value);
					return;
				}
				this.WriteValue(Convert.ToDateTime(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is Guid)
				{
					this.WriteValue((Guid)value);
					return;
				}
				this.WriteValue((byte[])value);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B558 File Offset: 0x00009758
		public void WriteToken(JsonToken token)
		{
			this.WriteToken(token, null);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B564 File Offset: 0x00009764
		internal virtual void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			int num = this.CalculateWriteTokenDepth(reader);
			do
			{
				if (writeDateConstructorAsDate && reader.TokenType == JsonToken.StartConstructor && string.Equals(reader.Value.ToString(), "Date", 4))
				{
					this.WriteConstructorDate(reader);
				}
				else if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					this.WriteToken(reader.TokenType, reader.Value);
				}
			}
			while (num - 1 < reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) && writeChildren && reader.Read());
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000B5F0 File Offset: 0x000097F0
		private int CalculateWriteTokenDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsStartToken(tokenType))
			{
				return reader.Depth + 1;
			}
			return reader.Depth;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000B620 File Offset: 0x00009820
		private void WriteConstructorDate(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				throw JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected Integer, got " + reader.TokenType, null);
			}
			DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime((long)reader.Value);
			if (!reader.Read())
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected EndConstructor, got " + reader.TokenType, null);
			}
			this.WriteValue(dateTime);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B6BC File Offset: 0x000098BC
		private void WriteEnd(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				this.WriteEndObject();
				return;
			case JsonContainerType.Array:
				this.WriteEndArray();
				return;
			case JsonContainerType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + type, null);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B70B File Offset: 0x0000990B
		private void AutoCompleteAll()
		{
			while (this.Top > 0)
			{
				this.WriteEnd();
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B71E File Offset: 0x0000991E
		private JsonToken GetCloseTokenForType(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				return JsonToken.EndObject;
			case JsonContainerType.Array:
				return JsonToken.EndArray;
			case JsonContainerType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw JsonWriterException.Create(this, "No close token for type: " + type, null);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B758 File Offset: 0x00009958
		private void AutoCompleteClose(JsonContainerType type)
		{
			int num = this.CalculateLevelsToComplete(type);
			for (int i = 0; i < num; i++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteNull();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
				this.UpdateCurrentState();
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B7C4 File Offset: 0x000099C4
		private int CalculateLevelsToComplete(JsonContainerType type)
		{
			int num = 0;
			if (this._currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = this.Top - 2;
				for (int i = num2; i >= 0; i--)
				{
					int num3 = num2 - i;
					if (this._stack[num3].Type == type)
					{
						num = i + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw JsonWriterException.Create(this, "No token to close.", null);
			}
			return num;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B82C File Offset: 0x00009A2C
		private void UpdateCurrentState()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this._currentState = JsonWriter.State.Start;
				return;
			case JsonContainerType.Object:
				this._currentState = JsonWriter.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonWriter.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonWriter.State.Array;
				return;
			default:
				throw JsonWriterException.Create(this, "Unknown JsonType: " + jsonContainerType, null);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00002C0D File Offset: 0x00000E0D
		protected virtual void WriteEnd(JsonToken token)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00002C0D File Offset: 0x00000E0D
		protected virtual void WriteIndent()
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00002C0D File Offset: 0x00000E0D
		protected virtual void WriteValueDelimiter()
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00002C0D File Offset: 0x00000E0D
		protected virtual void WriteIndentSpace()
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B890 File Offset: 0x00009A90
		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), this._currentState.ToString()), null);
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			if (this._formatting == Formatting.Indented)
			{
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteIndentSpace();
				}
				if (this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.ArrayStart || this._currentState == JsonWriter.State.Constructor || this._currentState == JsonWriter.State.ConstructorStart || (tokenBeingWritten == JsonToken.PropertyName && this._currentState != JsonWriter.State.Start))
				{
					this.WriteIndent();
				}
			}
			this._currentState = state;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B960 File Offset: 0x00009B60
		public virtual void WriteNull()
		{
			this.InternalWriteValue(JsonToken.Null);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B96A File Offset: 0x00009B6A
		public virtual void WriteUndefined()
		{
			this.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B974 File Offset: 0x00009B74
		public virtual void WriteRaw(string json)
		{
			this.InternalWriteRaw();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B97C File Offset: 0x00009B7C
		public virtual void WriteRawValue(string json)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B993 File Offset: 0x00009B93
		public virtual void WriteValue(string value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B99D File Offset: 0x00009B9D
		public virtual void WriteValue(int value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B99D File Offset: 0x00009B9D
		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B99D File Offset: 0x00009B9D
		public virtual void WriteValue(long value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B99D File Offset: 0x00009B9D
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B9A6 File Offset: 0x00009BA6
		public virtual void WriteValue(float value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B9A6 File Offset: 0x00009BA6
		public virtual void WriteValue(double value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B9AF File Offset: 0x00009BAF
		public virtual void WriteValue(bool value)
		{
			this.InternalWriteValue(JsonToken.Boolean);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B99D File Offset: 0x00009B9D
		public virtual void WriteValue(short value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B99D File Offset: 0x00009B9D
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B993 File Offset: 0x00009B93
		public virtual void WriteValue(char value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B99D File Offset: 0x00009B9D
		public virtual void WriteValue(byte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B99D File Offset: 0x00009B9D
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B9A6 File Offset: 0x00009BA6
		public virtual void WriteValue(decimal value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		public virtual void WriteValue(DateTime value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		public virtual void WriteValue(DateTimeOffset value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B993 File Offset: 0x00009B93
		public virtual void WriteValue(Guid value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B993 File Offset: 0x00009B93
		public virtual void WriteValue(TimeSpan value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B9C3 File Offset: 0x00009BC3
		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B9E2 File Offset: 0x00009BE2
		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000BA01 File Offset: 0x00009C01
		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000BA20 File Offset: 0x00009C20
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BA3F File Offset: 0x00009C3F
		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BA5E File Offset: 0x00009C5E
		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BA7D File Offset: 0x00009C7D
		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000BA9C File Offset: 0x00009C9C
		public virtual void WriteValue(short? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BABB File Offset: 0x00009CBB
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000BADA File Offset: 0x00009CDA
		public virtual void WriteValue(char? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BAF9 File Offset: 0x00009CF9
		public virtual void WriteValue(byte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000BB18 File Offset: 0x00009D18
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000BB37 File Offset: 0x00009D37
		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000BB56 File Offset: 0x00009D56
		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000BB75 File Offset: 0x00009D75
		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000BB94 File Offset: 0x00009D94
		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000BBB3 File Offset: 0x00009DB3
		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000BBD2 File Offset: 0x00009DD2
		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.Bytes);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000BBE6 File Offset: 0x00009DE6
		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000BC00 File Offset: 0x00009E00
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is BigInteger)
			{
				throw JsonWriter.CreateUnsupportedTypeException(this, value);
			}
			JsonWriter.WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000BC2E File Offset: 0x00009E2E
		public virtual void WriteComment(string text)
		{
			this.InternalWriteComment();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BC36 File Offset: 0x00009E36
		public virtual void WriteWhitespace(string ws)
		{
			this.InternalWriteWhitespace(ws);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BC3F File Offset: 0x00009E3F
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000BC4E File Offset: 0x00009E4E
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonWriter.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BC68 File Offset: 0x00009E68
		internal static void WriteValue(JsonWriter writer, PrimitiveTypeCode typeCode, object value)
		{
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
				writer.WriteValue((char)value);
				return;
			case PrimitiveTypeCode.CharNullable:
				writer.WriteValue((value == null) ? default(char?) : new char?((char)value));
				return;
			case PrimitiveTypeCode.Boolean:
				writer.WriteValue((bool)value);
				return;
			case PrimitiveTypeCode.BooleanNullable:
				writer.WriteValue((value == null) ? default(bool?) : new bool?((bool)value));
				return;
			case PrimitiveTypeCode.SByte:
				writer.WriteValue((sbyte)value);
				return;
			case PrimitiveTypeCode.SByteNullable:
				writer.WriteValue((value == null) ? default(sbyte?) : new sbyte?((sbyte)value));
				return;
			case PrimitiveTypeCode.Int16:
				writer.WriteValue((short)value);
				return;
			case PrimitiveTypeCode.Int16Nullable:
				writer.WriteValue((value == null) ? default(short?) : new short?((short)value));
				return;
			case PrimitiveTypeCode.UInt16:
				writer.WriteValue((ushort)value);
				return;
			case PrimitiveTypeCode.UInt16Nullable:
				writer.WriteValue((value == null) ? default(ushort?) : new ushort?((ushort)value));
				return;
			case PrimitiveTypeCode.Int32:
				writer.WriteValue((int)value);
				return;
			case PrimitiveTypeCode.Int32Nullable:
				writer.WriteValue((value == null) ? default(int?) : new int?((int)value));
				return;
			case PrimitiveTypeCode.Byte:
				writer.WriteValue((byte)value);
				return;
			case PrimitiveTypeCode.ByteNullable:
				writer.WriteValue((value == null) ? default(byte?) : new byte?((byte)value));
				return;
			case PrimitiveTypeCode.UInt32:
				writer.WriteValue((uint)value);
				return;
			case PrimitiveTypeCode.UInt32Nullable:
				writer.WriteValue((value == null) ? default(uint?) : new uint?((uint)value));
				return;
			case PrimitiveTypeCode.Int64:
				writer.WriteValue((long)value);
				return;
			case PrimitiveTypeCode.Int64Nullable:
				writer.WriteValue((value == null) ? default(long?) : new long?((long)value));
				return;
			case PrimitiveTypeCode.UInt64:
				writer.WriteValue((ulong)value);
				return;
			case PrimitiveTypeCode.UInt64Nullable:
				writer.WriteValue((value == null) ? default(ulong?) : new ulong?((ulong)value));
				return;
			case PrimitiveTypeCode.Single:
				writer.WriteValue((float)value);
				return;
			case PrimitiveTypeCode.SingleNullable:
				writer.WriteValue((value == null) ? default(float?) : new float?((float)value));
				return;
			case PrimitiveTypeCode.Double:
				writer.WriteValue((double)value);
				return;
			case PrimitiveTypeCode.DoubleNullable:
				writer.WriteValue((value == null) ? default(double?) : new double?((double)value));
				return;
			case PrimitiveTypeCode.DateTime:
				writer.WriteValue((DateTime)value);
				return;
			case PrimitiveTypeCode.DateTimeNullable:
				writer.WriteValue((value == null) ? default(DateTime?) : new DateTime?((DateTime)value));
				return;
			case PrimitiveTypeCode.DateTimeOffset:
				writer.WriteValue((DateTimeOffset)value);
				return;
			case PrimitiveTypeCode.DateTimeOffsetNullable:
				writer.WriteValue((value == null) ? default(DateTimeOffset?) : new DateTimeOffset?((DateTimeOffset)value));
				return;
			case PrimitiveTypeCode.Decimal:
				writer.WriteValue((decimal)value);
				return;
			case PrimitiveTypeCode.DecimalNullable:
				writer.WriteValue((value == null) ? default(decimal?) : new decimal?((decimal)value));
				return;
			case PrimitiveTypeCode.Guid:
				writer.WriteValue((Guid)value);
				return;
			case PrimitiveTypeCode.GuidNullable:
				writer.WriteValue((value == null) ? default(Guid?) : new Guid?((Guid)value));
				return;
			case PrimitiveTypeCode.TimeSpan:
				writer.WriteValue((TimeSpan)value);
				return;
			case PrimitiveTypeCode.TimeSpanNullable:
				writer.WriteValue((value == null) ? default(TimeSpan?) : new TimeSpan?((TimeSpan)value));
				return;
			case PrimitiveTypeCode.BigInteger:
				writer.WriteValue((BigInteger)value);
				return;
			case PrimitiveTypeCode.BigIntegerNullable:
				writer.WriteValue((value == null) ? default(BigInteger?) : new BigInteger?((BigInteger)value));
				return;
			case PrimitiveTypeCode.Uri:
				writer.WriteValue((Uri)value);
				return;
			case PrimitiveTypeCode.String:
				writer.WriteValue((string)value);
				return;
			case PrimitiveTypeCode.Bytes:
				writer.WriteValue((byte[])value);
				return;
			case PrimitiveTypeCode.DBNull:
				writer.WriteNull();
				return;
			default:
			{
				IConvertible convertible = value as IConvertible;
				if (convertible != null)
				{
					TypeInformation typeInformation = ConvertUtils.GetTypeInformation(convertible);
					PrimitiveTypeCode primitiveTypeCode = ((typeInformation.TypeCode == PrimitiveTypeCode.Object) ? PrimitiveTypeCode.String : typeInformation.TypeCode);
					Type type = ((typeInformation.TypeCode == PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type);
					object obj = convertible.ToType(type, CultureInfo.InvariantCulture);
					JsonWriter.WriteValue(writer, primitiveTypeCode, obj);
					return;
				}
				throw JsonWriter.CreateUnsupportedTypeException(writer, value);
			}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		private static JsonWriterException CreateUnsupportedTypeException(JsonWriter writer, object value)
		{
			return JsonWriterException.Create(writer, "Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C11C File Offset: 0x0000A31C
		protected void SetWriteState(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.StartObject:
				this.InternalWriteStart(token, JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this.InternalWriteStart(token, JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this.InternalWriteStart(token, JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				if (!(value is string))
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				this.InternalWritePropertyName((string)value);
				return;
			case JsonToken.Comment:
				this.InternalWriteComment();
				return;
			case JsonToken.Raw:
				this.InternalWriteRaw();
				return;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.InternalWriteValue(token);
				return;
			case JsonToken.EndObject:
				this.InternalWriteEnd(JsonContainerType.Object);
				return;
			case JsonToken.EndArray:
				this.InternalWriteEnd(JsonContainerType.Array);
				return;
			case JsonToken.EndConstructor:
				this.InternalWriteEnd(JsonContainerType.Constructor);
				return;
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C1EF File Offset: 0x0000A3EF
		internal void InternalWriteEnd(JsonContainerType container)
		{
			this.AutoCompleteClose(container);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		internal void InternalWritePropertyName(string name)
		{
			this._currentPosition.PropertyName = name;
			this.AutoComplete(JsonToken.PropertyName);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00002C0D File Offset: 0x00000E0D
		internal void InternalWriteRaw()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000C20D File Offset: 0x0000A40D
		internal void InternalWriteStart(JsonToken token, JsonContainerType container)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
			this.Push(container);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C223 File Offset: 0x0000A423
		internal void InternalWriteValue(JsonToken token)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C232 File Offset: 0x0000A432
		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw JsonWriterException.Create(this, "Only white space characters should be used.", null);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C24C File Offset: 0x0000A44C
		internal void InternalWriteComment()
		{
			this.AutoComplete(JsonToken.Comment);
		}

		// Token: 0x04000123 RID: 291
		private static readonly JsonWriter.State[][] StateArray = JsonWriter.BuildStateArray();

		// Token: 0x04000124 RID: 292
		internal static readonly JsonWriter.State[][] StateArrayTempate = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		// Token: 0x04000125 RID: 293
		private List<JsonPosition> _stack;

		// Token: 0x04000126 RID: 294
		private JsonPosition _currentPosition;

		// Token: 0x04000127 RID: 295
		private JsonWriter.State _currentState;

		// Token: 0x04000128 RID: 296
		private Formatting _formatting;

		// Token: 0x04000129 RID: 297
		[CompilerGenerated]
		private bool <CloseOutput>k__BackingField;

		// Token: 0x0400012A RID: 298
		[CompilerGenerated]
		private bool <AutoCompleteOnClose>k__BackingField;

		// Token: 0x0400012B RID: 299
		private DateFormatHandling _dateFormatHandling;

		// Token: 0x0400012C RID: 300
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x0400012D RID: 301
		private StringEscapeHandling _stringEscapeHandling;

		// Token: 0x0400012E RID: 302
		private FloatFormatHandling _floatFormatHandling;

		// Token: 0x0400012F RID: 303
		private string _dateFormatString;

		// Token: 0x04000130 RID: 304
		private CultureInfo _culture;

		// Token: 0x02000107 RID: 263
		internal enum State
		{
			// Token: 0x0400042E RID: 1070
			Start,
			// Token: 0x0400042F RID: 1071
			Property,
			// Token: 0x04000430 RID: 1072
			ObjectStart,
			// Token: 0x04000431 RID: 1073
			Object,
			// Token: 0x04000432 RID: 1074
			ArrayStart,
			// Token: 0x04000433 RID: 1075
			Array,
			// Token: 0x04000434 RID: 1076
			ConstructorStart,
			// Token: 0x04000435 RID: 1077
			Constructor,
			// Token: 0x04000436 RID: 1078
			Closed,
			// Token: 0x04000437 RID: 1079
			Error
		}
	}
}
