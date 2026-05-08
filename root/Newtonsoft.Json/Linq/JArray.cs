using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B8 RID: 184
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00024E6B File Offset: 0x0002306B
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00024E73 File Offset: 0x00023073
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00024E76 File Offset: 0x00023076
		public JArray()
		{
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00024E89 File Offset: 0x00023089
		public JArray(JArray other)
			: base(other)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00024E9D File Offset: 0x0002309D
		public JArray(params object[] content)
			: this(content)
		{
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00024EA6 File Offset: 0x000230A6
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00024EC0 File Offset: 0x000230C0
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00024EE0 File Offset: 0x000230E0
		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00024EE8 File Offset: 0x000230E8
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00024EF4 File Offset: 0x000230F4
		public new static JArray Load(JsonReader reader, JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00024F68 File Offset: 0x00023168
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00024F74 File Offset: 0x00023174
		public new static JArray Parse(string json, JsonLoadSettings settings)
		{
			JArray jarray2;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				jarray2 = jarray;
			}
			return jarray2;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00024FBC File Offset: 0x000231BC
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00024FCC File Offset: 0x000231CC
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00025010 File Offset: 0x00023210
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170001DD RID: 477
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170001DE RID: 478
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000250CB File Offset: 0x000232CB
		internal override int IndexOfItem(JToken item)
		{
			return this._values.IndexOfReference(item);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000250DC File Offset: 0x000232DC
		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			IEnumerable enumerable = ((base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null);
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000240F7 File Offset: 0x000222F7
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00024100 File Offset: 0x00022300
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002410B File Offset: 0x0002230B
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00025110 File Offset: 0x00023310
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00024127 File Offset: 0x00022327
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00023DB2 File Offset: 0x00021FB2
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00024130 File Offset: 0x00022330
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00024139 File Offset: 0x00022339
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00024143 File Offset: 0x00022343
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00024D98 File Offset: 0x00022F98
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x04000348 RID: 840
		private readonly List<JToken> _values = new List<JToken>();
	}
}
