using System;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BA RID: 186
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00025615 File Offset: 0x00023815
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002561D File Offset: 0x0002381D
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00025634 File Offset: 0x00023834
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00025655 File Offset: 0x00023855
		public JTokenWriter()
		{
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00002C0D File Offset: 0x00000E0D
		public override void Flush()
		{
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002565D File Offset: 0x0002385D
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00025665 File Offset: 0x00023865
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00025678 File Offset: 0x00023878
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000256A8 File Offset: 0x000238A8
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000256F9 File Offset: 0x000238F9
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002570C File Offset: 0x0002390C
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00025721 File Offset: 0x00023921
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00025729 File Offset: 0x00023929
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00025756 File Offset: 0x00023956
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00025768 File Offset: 0x00023968
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				this._parent.Add(value);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					return;
				}
			}
			else
			{
				this._value = value ?? JValue.CreateNull();
				this._current = this._value;
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000257D6 File Offset: 0x000239D6
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000257F7 File Offset: 0x000239F7
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00025808 File Offset: 0x00023A08
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00025819 File Offset: 0x00023A19
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002582F File Offset: 0x00023A2F
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00025845 File Offset: 0x00023A45
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00025857 File Offset: 0x00023A57
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002586D File Offset: 0x00023A6D
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00025883 File Offset: 0x00023A83
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00025899 File Offset: 0x00023A99
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000258AF File Offset: 0x00023AAF
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000258C5 File Offset: 0x00023AC5
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000258DB File Offset: 0x00023ADB
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000258F2 File Offset: 0x00023AF2
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00025908 File Offset: 0x00023B08
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00025920 File Offset: 0x00023B20
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddValue(text, JsonToken.String);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002594C File Offset: 0x00023B4C
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00025962 File Offset: 0x00023B62
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00025978 File Offset: 0x00023B78
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002598E File Offset: 0x00023B8E
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000259B3 File Offset: 0x00023BB3
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000259CA File Offset: 0x00023BCA
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000259DC File Offset: 0x00023BDC
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000259F3 File Offset: 0x00023BF3
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00025A0A File Offset: 0x00023C0A
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00025A1C File Offset: 0x00023C1C
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken();
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = jtoken as JContainer;
					this._value = jtoken as JValue;
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x0400034D RID: 845
		private JContainer _token;

		// Token: 0x0400034E RID: 846
		private JContainer _parent;

		// Token: 0x0400034F RID: 847
		private JValue _value;

		// Token: 0x04000350 RID: 848
		private JToken _current;
	}
}
