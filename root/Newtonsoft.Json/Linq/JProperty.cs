using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BC RID: 188
	public class JProperty : JContainer
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00027FB8 File Offset: 0x000261B8
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00027FC0 File Offset: 0x000261C0
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00027FC8 File Offset: 0x000261C8
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00027FD8 File Offset: 0x000261D8
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken jtoken = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, jtoken, false);
					return;
				}
				this.SetItem(0, jtoken);
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00028015 File Offset: 0x00026215
		public JProperty(JProperty other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00028035 File Offset: 0x00026235
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00028048 File Offset: 0x00026248
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000280A2 File Offset: 0x000262A2
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000280A2 File Offset: 0x000262A2
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000280C2 File Offset: 0x000262C2
		internal override int IndexOfItem(JToken item)
		{
			return this._content.IndexOf(item);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000280D0 File Offset: 0x000262D0
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			base.InsertItem(0, item, false);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002810F File Offset: 0x0002630F
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002811C File Offset: 0x0002631C
		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = ((jproperty != null) ? jproperty.Value : null);
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000280A2 File Offset: 0x000262A2
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00028150 File Offset: 0x00026350
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00028183 File Offset: 0x00026383
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002818B File Offset: 0x0002638B
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002818E File Offset: 0x0002638E
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000281B3 File Offset: 0x000263B3
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000281C0 File Offset: 0x000263C0
		public JProperty(string name, object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00028210 File Offset: 0x00026410
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00028242 File Offset: 0x00026442
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ ((this.Value != null) ? this.Value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00028266 File Offset: 0x00026466
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00028270 File Offset: 0x00026470
		public new static JProperty Load(JsonReader reader, JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x04000360 RID: 864
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x04000361 RID: 865
		private readonly string _name;

		// Token: 0x02000163 RID: 355
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06000D7B RID: 3451 RVA: 0x00032760 File Offset: 0x00030960
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x06000D7C RID: 3452 RVA: 0x0003276F File Offset: 0x0003096F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00032777 File Offset: 0x00030977
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x06000D7E RID: 3454 RVA: 0x00032780 File Offset: 0x00030980
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x00032789 File Offset: 0x00030989
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x00032794 File Offset: 0x00030994
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06000D81 RID: 3457 RVA: 0x000327A7 File Offset: 0x000309A7
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000327BC File Offset: 0x000309BC
			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0000F9CA File Offset: 0x0000DBCA
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000D84 RID: 3460 RVA: 0x000327C9 File Offset: 0x000309C9
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06000D85 RID: 3461 RVA: 0x000327D7 File Offset: 0x000309D7
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06000D86 RID: 3462 RVA: 0x000327E3 File Offset: 0x000309E3
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x170002A1 RID: 673
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						return null;
					}
					return this._token;
				}
				set
				{
					if (index == 0)
					{
						this._token = value;
					}
				}
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x00008020 File Offset: 0x00006220
			public JPropertyList()
			{
			}

			// Token: 0x04000522 RID: 1314
			internal JToken _token;

			// Token: 0x0200017C RID: 380
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__1 : IEnumerator<JToken>, IDisposable, IEnumerator
			{
				// Token: 0x06000DF8 RID: 3576 RVA: 0x00034445 File Offset: 0x00032645
				[DebuggerHidden]
				public <GetEnumerator>d__1(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x06000DF9 RID: 3577 RVA: 0x00002C0D File Offset: 0x00000E0D
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000DFA RID: 3578 RVA: 0x00034454 File Offset: 0x00032654
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -1;
					}
					else
					{
						this.<>1__state = -1;
						if (this._token != null)
						{
							this.<>2__current = this._token;
							this.<>1__state = 1;
							return true;
						}
					}
					return false;
				}

				// Token: 0x170002B5 RID: 693
				// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000344A7 File Offset: 0x000326A7
				JToken IEnumerator<JToken>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000DFC RID: 3580 RVA: 0x00024289 File Offset: 0x00022489
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170002B6 RID: 694
				// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000344A7 File Offset: 0x000326A7
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x040005A4 RID: 1444
				private int <>1__state;

				// Token: 0x040005A5 RID: 1445
				private JToken <>2__current;

				// Token: 0x040005A6 RID: 1446
				public JProperty.JPropertyList <>4__this;
			}
		}
	}
}
