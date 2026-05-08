using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B5 RID: 181
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable, ITypedList, IBindingList, IList, ICollection, INotifyCollectionChanged
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600085E RID: 2142 RVA: 0x000234F3 File Offset: 0x000216F3
		// (remove) Token: 0x0600085F RID: 2143 RVA: 0x0002350C File Offset: 0x0002170C
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Combine(this._listChanged, value);
			}
			remove
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Remove(this._listChanged, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000860 RID: 2144 RVA: 0x00023525 File Offset: 0x00021725
		// (remove) Token: 0x06000861 RID: 2145 RVA: 0x0002353E File Offset: 0x0002173E
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Combine(this._addingNew, value);
			}
			remove
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Remove(this._addingNew, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000862 RID: 2146 RVA: 0x00023557 File Offset: 0x00021757
		// (remove) Token: 0x06000863 RID: 2147 RVA: 0x00023570 File Offset: 0x00021770
		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000864 RID: 2148
		protected abstract IList<JToken> ChildrenTokens { get; }

		// Token: 0x06000865 RID: 2149 RVA: 0x00023589 File Offset: 0x00021789
		internal JContainer()
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00023594 File Offset: 0x00021794
		internal JContainer(JContainer other)
			: this()
		{
			ValidationUtils.ArgumentNotNull(other, "other");
			int num = 0;
			foreach (JToken jtoken in other)
			{
				this.AddInternal(num, jtoken, false);
				num++;
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000235F8 File Offset: 0x000217F8
		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002361D File Offset: 0x0002181D
		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00023624 File Offset: 0x00021824
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNew = this._addingNew;
			if (addingNew == null)
			{
				return;
			}
			addingNew.Invoke(this, e);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00023638 File Offset: 0x00021838
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChanged = this._listChanged;
			if (listChanged != null)
			{
				this._busy = true;
				try
				{
					listChanged.Invoke(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00023678 File Offset: 0x00021878
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this._collectionChanged;
			if (collectionChanged != null)
			{
				this._busy = true;
				try
				{
					collectionChanged.Invoke(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000236B8 File Offset: 0x000218B8
		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000236C8 File Offset: 0x000218C8
		internal bool ContentsEqual(JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			IList<JToken> childrenTokens = this.ChildrenTokens;
			IList<JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00023724 File Offset: 0x00021924
		public override JToken First
		{
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002374C File Offset: 0x0002194C
		public override JToken Last
		{
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00023776 File Offset: 0x00021976
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00023783 File Offset: 0x00021983
		public override IEnumerable<T> Values<T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00023790 File Offset: 0x00021990
		public IEnumerable<JToken> Descendants()
		{
			return this.GetDescendants(false);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00023799 File Offset: 0x00021999
		public IEnumerable<JToken> DescendantsAndSelf()
		{
			return this.GetDescendants(true);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000237A2 File Offset: 0x000219A2
		internal IEnumerable<JToken> GetDescendants(bool self)
		{
			if (self)
			{
				yield return this;
			}
			foreach (JToken o in this.ChildrenTokens)
			{
				yield return o;
				JContainer jcontainer = o as JContainer;
				if (jcontainer != null)
				{
					foreach (JToken jtoken in jcontainer.Descendants())
					{
						yield return jtoken;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				o = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000237B9 File Offset: 0x000219B9
		internal bool IsMultiContent(object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000237E1 File Offset: 0x000219E1
		internal JToken EnsureParentToken(JToken item, bool skipParentCheck)
		{
			if (item == null)
			{
				return JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				item = item.CloneToken();
			}
			return item;
		}

		// Token: 0x06000877 RID: 2167
		internal abstract int IndexOfItem(JToken item);

		// Token: 0x06000878 RID: 2168 RVA: 0x00023818 File Offset: 0x00021A18
		internal virtual void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, skipParentCheck);
			JToken jtoken = ((index == 0) ? null : childrenTokens[index - 1]);
			JToken jtoken2 = ((index == childrenTokens.Count) ? null : childrenTokens[index]);
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			childrenTokens.Insert(index, item);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(1, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(0, item, index));
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000238E0 File Offset: 0x00021AE0
		internal virtual void RemoveItemAt(int index)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = childrenTokens[index];
			JToken jtoken2 = ((index == 0) ? null : childrenTokens[index - 1]);
			JToken jtoken3 = ((index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1]);
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			childrenTokens.RemoveAt(index);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(2, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(1, jtoken, index));
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000239B4 File Offset: 0x00021BB4
		internal virtual bool RemoveItem(JToken item)
		{
			int num = this.IndexOfItem(item);
			if (num >= 0)
			{
				this.RemoveItemAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000239D7 File Offset: 0x00021BD7
		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000239E8 File Offset: 0x00021BE8
		internal virtual void SetItem(int index, JToken item)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = childrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, false);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = ((index == 0) ? null : childrenTokens[index - 1]);
			JToken jtoken3 = ((index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1]);
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			childrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(4, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(2, item, jtoken, index));
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00023AF0 File Offset: 0x00021CF0
		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			IList<JToken> childrenTokens = this.ChildrenTokens;
			foreach (JToken jtoken in childrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			childrenTokens.Clear();
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(0, -1));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(4));
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00023B80 File Offset: 0x00021D80
		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int num = this.IndexOfItem(existing);
			this.SetItem(num, replacement);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00023BAA File Offset: 0x00021DAA
		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00023BBC File Offset: 0x00021DBC
		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				array.SetValue(jtoken, arrayIndex + num);
				num++;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00023C68 File Offset: 0x00021E68
		internal static bool IsTokenUnchanged(JToken currentValue, JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			return jvalue != null && ((jvalue.Type == JTokenType.Null && newValue == null) || jvalue.Equals(newValue));
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00023C97 File Offset: 0x00021E97
		internal virtual void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00023CCE File Offset: 0x00021ECE
		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content, false);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00023CE3 File Offset: 0x00021EE3
		internal void AddAndSkipParentCheck(JToken token)
		{
			this.AddInternal(this.ChildrenTokens.Count, token, true);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00023CF8 File Offset: 0x00021EF8
		public void AddFirst(object content)
		{
			this.AddInternal(0, content, false);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00023D04 File Offset: 0x00021F04
		internal void AddInternal(int index, object content, bool skipParentCheck)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						this.AddInternal(num, obj, skipParentCheck);
						num++;
					}
					return;
				}
			}
			JToken jtoken = JContainer.CreateFromContent(content);
			this.InsertItem(index, jtoken, skipParentCheck);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00023D7C File Offset: 0x00021F7C
		internal static JToken CreateFromContent(object content)
		{
			JToken jtoken = content as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			return new JValue(content);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00023D9B File Offset: 0x00021F9B
		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00023DA3 File Offset: 0x00021FA3
		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00023DB2 File Offset: 0x00021FB2
		public void RemoveAll()
		{
			this.ClearItems();
		}

		// Token: 0x0600088B RID: 2187
		internal abstract void MergeItem(object content, JsonMergeSettings settings);

		// Token: 0x0600088C RID: 2188 RVA: 0x00023DBA File Offset: 0x00021FBA
		public void Merge(object content)
		{
			this.MergeItem(content, new JsonMergeSettings());
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00023DC8 File Offset: 0x00021FC8
		public void Merge(object content, JsonMergeSettings settings)
		{
			this.MergeItem(content, settings);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00023DD4 File Offset: 0x00021FD4
		internal void ReadTokenFrom(JsonReader reader, JsonLoadSettings options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			this.ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00023E44 File Offset: 0x00022044
		internal void ReadContentFrom(JsonReader r, JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo jsonLineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				JProperty jproperty = jcontainer as JProperty;
				if (((jproperty != null) ? jproperty.Value : null) != null)
				{
					if (jcontainer == this)
					{
						break;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_0226;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(jsonLineInfo, settings);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_0226;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(jsonLineInfo, settings);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_0226;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(jsonLineInfo, settings);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_0226;
				}
				case JsonToken.PropertyName:
				{
					string text = r.Value.ToString();
					JProperty jproperty2 = new JProperty(text);
					jproperty2.SetLineInfo(jsonLineInfo, settings);
					JProperty jproperty3 = ((JObject)jcontainer).Property(text);
					if (jproperty3 == null)
					{
						jcontainer.Add(jproperty2);
					}
					else
					{
						jproperty3.Replace(jproperty2);
					}
					jcontainer = jproperty2;
					goto IL_0226;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(r.Value.ToString());
						jvalue.SetLineInfo(jsonLineInfo, settings);
						jcontainer.Add(jvalue);
						goto IL_0226;
					}
					goto IL_0226;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(jsonLineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_0226;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(jsonLineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_0226;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(jsonLineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_0226;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_0226;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_0226;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_0226;
				}
				goto Block_4;
				IL_0226:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, r.TokenType));
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00024084 File Offset: 0x00022284
		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000240D8 File Offset: 0x000222D8
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000240DF File Offset: 0x000222DF
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICustomTypeDescriptor customTypeDescriptor = this.First as ICustomTypeDescriptor;
			if (customTypeDescriptor == null)
			{
				return null;
			}
			return customTypeDescriptor.GetProperties();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000240F7 File Offset: 0x000222F7
		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00024100 File Offset: 0x00022300
		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002410B File Offset: 0x0002230B
		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00024114 File Offset: 0x00022314
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0002411D File Offset: 0x0002231D
		JToken IList<JToken>.Item
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

		// Token: 0x06000898 RID: 2200 RVA: 0x00024127 File Offset: 0x00022327
		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00023DB2 File Offset: 0x00021FB2
		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00024130 File Offset: 0x00022330
		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00024139 File Offset: 0x00022339
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00024143 File Offset: 0x00022343
		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002414C File Offset: 0x0002234C
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			JToken jtoken = value as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00024174 File Offset: 0x00022374
		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00023DB2 File Offset: 0x00021FB2
		void IList.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002418B File Offset: 0x0002238B
		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002419A File Offset: 0x0002239A
		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000241A9 File Offset: 0x000223A9
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value), false);
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000241BA File Offset: 0x000223BA
		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002410B File Offset: 0x0002230B
		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00024114 File Offset: 0x00022314
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000241CA File Offset: 0x000223CA
		object IList.Item
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00024139 File Offset: 0x00022339
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000241DA File Offset: 0x000223DA
		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000241E7 File Offset: 0x000223E7
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00002C0D File Offset: 0x00000E0D
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002420C File Offset: 0x0002240C
		object IBindingList.AddNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			this.OnAddingNew(addingNewEventArgs);
			if (addingNewEventArgs.NewObject == null)
			{
				throw new JsonException("Could not determine new value to add to '{0}'.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			if (!(addingNewEventArgs.NewObject is JToken))
			{
				throw new JsonException("New item to be added to collection must be compatible with {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JToken)));
			}
			JToken jtoken = (JToken)addingNewEventArgs.NewObject;
			this.Add(jtoken);
			return jtoken;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000071F5 File Offset: 0x000053F5
		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x000071F5 File Offset: 0x000053F5
		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x000071F5 File Offset: 0x000053F5
		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00024289 File Offset: 0x00022489
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00024289 File Offset: 0x00022489
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00002C0D File Offset: 0x00000E0D
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00024289 File Offset: 0x00022489
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00024290 File Offset: 0x00022490
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000071F5 File Offset: 0x000053F5
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00024294 File Offset: 0x00022494
		internal static void MergeEnumerableContent(JContainer target, IEnumerable content, JsonMergeSettings settings)
		{
			switch (settings.MergeArrayHandling)
			{
			case MergeArrayHandling.Concat:
			{
				using (IEnumerator enumerator = content.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						JToken jtoken = (JToken)obj;
						target.Add(jtoken);
					}
					return;
				}
				break;
			}
			case MergeArrayHandling.Union:
				break;
			case MergeArrayHandling.Replace:
				goto IL_00B6;
			case MergeArrayHandling.Merge:
				goto IL_00FB;
			default:
				goto IL_018C;
			}
			HashSet<JToken> hashSet = new HashSet<JToken>(target, JToken.EqualityComparer);
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					JToken jtoken2 = (JToken)obj2;
					if (hashSet.Add(jtoken2))
					{
						target.Add(jtoken2);
					}
				}
				return;
			}
			IL_00B6:
			target.ClearItems();
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					JToken jtoken3 = (JToken)obj3;
					target.Add(jtoken3);
				}
				return;
			}
			IL_00FB:
			int num = 0;
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj4 = enumerator.Current;
					if (num < target.Count)
					{
						JContainer jcontainer = target[num] as JContainer;
						if (jcontainer != null)
						{
							jcontainer.Merge(obj4, settings);
						}
						else if (obj4 != null)
						{
							JToken jtoken4 = JContainer.CreateFromContent(obj4);
							if (jtoken4.Type != JTokenType.Null)
							{
								target[num] = jtoken4;
							}
						}
					}
					else
					{
						target.Add(obj4);
					}
					num++;
				}
				return;
			}
			IL_018C:
			throw new ArgumentOutOfRangeException("settings", "Unexpected merge array handling when merging JSON.");
		}

		// Token: 0x0400033E RID: 830
		internal ListChangedEventHandler _listChanged;

		// Token: 0x0400033F RID: 831
		internal AddingNewEventHandler _addingNew;

		// Token: 0x04000340 RID: 832
		internal NotifyCollectionChangedEventHandler _collectionChanged;

		// Token: 0x04000341 RID: 833
		private object _syncRoot;

		// Token: 0x04000342 RID: 834
		private bool _busy;

		// Token: 0x02000159 RID: 345
		[CompilerGenerated]
		private sealed class <GetDescendants>d__34 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000D3A RID: 3386 RVA: 0x00031CDC File Offset: 0x0002FEDC
			[DebuggerHidden]
			public <GetDescendants>d__34(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x00031CFC File Offset: 0x0002FEFC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num <= -3)
				{
					if (num != -4 && num != -3)
					{
						return;
					}
				}
				else if (num != 2 && num != 3)
				{
					return;
				}
				try
				{
					if (num == -4 || num == 3)
					{
						try
						{
						}
						finally
						{
							this.<>m__Finally2();
						}
					}
				}
				finally
				{
					this.<>m__Finally1();
				}
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x00031D64 File Offset: 0x0002FF64
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						if (self)
						{
							this.<>2__current = this;
							this.<>1__state = 1;
							return true;
						}
						break;
					case 1:
						this.<>1__state = -1;
						break;
					case 2:
					{
						this.<>1__state = -3;
						JContainer jcontainer = o as JContainer;
						if (jcontainer != null)
						{
							enumerator2 = jcontainer.Descendants().GetEnumerator();
							this.<>1__state = -4;
							goto IL_00FA;
						}
						goto IL_0114;
					}
					case 3:
						this.<>1__state = -4;
						goto IL_00FA;
					default:
						return false;
					}
					enumerator = this.ChildrenTokens.GetEnumerator();
					this.<>1__state = -3;
					goto IL_011B;
					IL_00FA:
					if (enumerator2.MoveNext())
					{
						JToken jtoken = enumerator2.Current;
						this.<>2__current = jtoken;
						this.<>1__state = 3;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_0114:
					o = null;
					IL_011B:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						o = enumerator.Current;
						this.<>2__current = o;
						this.<>1__state = 2;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x00031ED4 File Offset: 0x000300D4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x00031EF0 File Offset: 0x000300F0
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00031F0D File Offset: 0x0003010D
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D40 RID: 3392 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00031F0D File Offset: 0x0003010D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D42 RID: 3394 RVA: 0x00031F18 File Offset: 0x00030118
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				JContainer.<GetDescendants>d__34 <GetDescendants>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDescendants>d__ = this;
				}
				else
				{
					<GetDescendants>d__ = new JContainer.<GetDescendants>d__34(0);
					<GetDescendants>d__.<>4__this = this;
				}
				<GetDescendants>d__.self = self;
				return <GetDescendants>d__;
			}

			// Token: 0x06000D43 RID: 3395 RVA: 0x00031F6C File Offset: 0x0003016C
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x040004F2 RID: 1266
			private int <>1__state;

			// Token: 0x040004F3 RID: 1267
			private JToken <>2__current;

			// Token: 0x040004F4 RID: 1268
			private int <>l__initialThreadId;

			// Token: 0x040004F5 RID: 1269
			private bool self;

			// Token: 0x040004F6 RID: 1270
			public bool <>3__self;

			// Token: 0x040004F7 RID: 1271
			public JContainer <>4__this;

			// Token: 0x040004F8 RID: 1272
			private JToken <o>5__1;

			// Token: 0x040004F9 RID: 1273
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x040004FA RID: 1274
			private IEnumerator<JToken> <>7__wrap2;
		}
	}
}
