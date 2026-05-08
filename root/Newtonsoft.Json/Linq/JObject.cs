using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B7 RID: 183
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged, ICustomTypeDescriptor, INotifyPropertyChanging
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00024529 File Offset: 0x00022729
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060008C7 RID: 2247 RVA: 0x00024534 File Offset: 0x00022734
		// (remove) Token: 0x060008C8 RID: 2248 RVA: 0x0002456C File Offset: 0x0002276C
		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060008C9 RID: 2249 RVA: 0x000245A4 File Offset: 0x000227A4
		// (remove) Token: 0x060008CA RID: 2250 RVA: 0x000245DC File Offset: 0x000227DC
		public event PropertyChangingEventHandler PropertyChanging
		{
			[CompilerGenerated]
			add
			{
				PropertyChangingEventHandler propertyChangingEventHandler = this.PropertyChanging;
				PropertyChangingEventHandler propertyChangingEventHandler2;
				do
				{
					propertyChangingEventHandler2 = propertyChangingEventHandler;
					PropertyChangingEventHandler propertyChangingEventHandler3 = (PropertyChangingEventHandler)Delegate.Combine(propertyChangingEventHandler2, value);
					propertyChangingEventHandler = Interlocked.CompareExchange<PropertyChangingEventHandler>(ref this.PropertyChanging, propertyChangingEventHandler3, propertyChangingEventHandler2);
				}
				while (propertyChangingEventHandler != propertyChangingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangingEventHandler propertyChangingEventHandler = this.PropertyChanging;
				PropertyChangingEventHandler propertyChangingEventHandler2;
				do
				{
					propertyChangingEventHandler2 = propertyChangingEventHandler;
					PropertyChangingEventHandler propertyChangingEventHandler3 = (PropertyChangingEventHandler)Delegate.Remove(propertyChangingEventHandler2, value);
					propertyChangingEventHandler = Interlocked.CompareExchange<PropertyChangingEventHandler>(ref this.PropertyChanging, propertyChangingEventHandler3, propertyChangingEventHandler2);
				}
				while (propertyChangingEventHandler != propertyChangingEventHandler2);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00024611 File Offset: 0x00022811
		public JObject()
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00024624 File Offset: 0x00022824
		public JObject(JObject other)
			: base(other)
		{
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00024638 File Offset: 0x00022838
		public JObject(params object[] content)
			: this(content)
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00024641 File Offset: 0x00022841
		public JObject(object content)
		{
			this.Add(content);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002465C File Offset: 0x0002285C
		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && this._properties.Compare(jobject._properties);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00024686 File Offset: 0x00022886
		internal override int IndexOfItem(JToken item)
		{
			return this._properties.IndexOfReference(item);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00024694 File Offset: 0x00022894
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			base.InsertItem(index, item, skipParentCheck);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000246AC File Offset: 0x000228AC
		internal override void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name, base.GetType()));
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002474C File Offset: 0x0002294C
		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			JObject jobject = content as JObject;
			if (jobject == null)
			{
				return;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
			{
				JProperty jproperty = this.Property(keyValuePair.Key);
				if (jproperty == null)
				{
					this.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else if (keyValuePair.Value != null)
				{
					JContainer jcontainer = jproperty.Value as JContainer;
					if (jcontainer == null || jcontainer.Type != keyValuePair.Value.Type)
					{
						if (keyValuePair.Value.Type != JTokenType.Null || (settings != null && settings.MergeNullValueHandling == MergeNullValueHandling.Merge))
						{
							jproperty.Value = keyValuePair.Value;
						}
					}
					else
					{
						jcontainer.Merge(keyValuePair.Value, settings);
					}
				}
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00024830 File Offset: 0x00022A30
		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(4, this.IndexOfItem(childProperty)));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(2, childProperty, childProperty, this.IndexOfItem(childProperty)));
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00024881 File Offset: 0x00022A81
		internal void InternalPropertyChanging(JProperty childProperty)
		{
			this.OnPropertyChanging(childProperty.Name);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002488F File Offset: 0x00022A8F
		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000071F5 File Offset: 0x000053F5
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00024897 File Offset: 0x00022A97
		public IEnumerable<JProperty> Properties()
		{
			return Enumerable.Cast<JProperty>(this._properties);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000248A4 File Offset: 0x00022AA4
		public JProperty Property(string name)
		{
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			this._properties.TryGetValue(name, out jtoken);
			return (JProperty)jtoken;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000248CB File Offset: 0x00022ACB
		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(Enumerable.Select<JProperty, JToken>(this.Properties(), (JProperty p) => p.Value));
		}

		// Token: 0x170001D6 RID: 470
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[text];
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[text] = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		public JToken this[string propertyName]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			set
			{
				JProperty jproperty = this.Property(propertyName);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.OnPropertyChanging(propertyName);
				this.Add(new JProperty(propertyName, value));
				this.OnPropertyChanged(propertyName);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000249DF File Offset: 0x00022BDF
		public new static JObject Load(JsonReader reader)
		{
			return JObject.Load(reader, null);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000249E8 File Offset: 0x00022BE8
		public new static JObject Load(JsonReader reader, JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo, settings);
			jobject.ReadTokenFrom(reader, settings);
			return jobject;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00024A67 File Offset: 0x00022C67
		public new static JObject Parse(string json)
		{
			return JObject.Parse(json, null);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00024A70 File Offset: 0x00022C70
		public new static JObject Parse(string json, JsonLoadSettings settings)
		{
			JObject jobject2;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JObject jobject = JObject.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				jobject2 = jobject;
			}
			return jobject2;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00024AB8 File Offset: 0x00022CB8
		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00024AC8 File Offset: 0x00022CC8
		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken != null && jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JObject)jtoken;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00024B10 File Offset: 0x00022D10
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < this._properties.Count; i++)
			{
				this._properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00024B52 File Offset: 0x00022D52
		public JToken GetValue(string propertyName)
		{
			return this.GetValue(propertyName, 4);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00024B5C File Offset: 0x00022D5C
		public JToken GetValue(string propertyName, StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			JProperty jproperty = this.Property(propertyName);
			if (jproperty != null)
			{
				return jproperty.Value;
			}
			if (comparison != 4)
			{
				foreach (JToken jtoken in this._properties)
				{
					JProperty jproperty2 = (JProperty)jtoken;
					if (string.Equals(jproperty2.Name, propertyName, comparison))
					{
						return jproperty2.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00024BE0 File Offset: 0x00022DE0
		public bool TryGetValue(string propertyName, StringComparison comparison, out JToken value)
		{
			value = this.GetValue(propertyName, comparison);
			return value != null;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00024BF1 File Offset: 0x00022DF1
		public void Add(string propertyName, JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00024C00 File Offset: 0x00022E00
		bool IDictionary<string, JToken>.ContainsKey(string key)
		{
			return this._properties.Contains(key);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00024C0E File Offset: 0x00022E0E
		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Keys;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00024C1C File Offset: 0x00022E1C
		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00024C40 File Offset: 0x00022E40
		public bool TryGetValue(string propertyName, out JToken value)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00024C66 File Offset: 0x00022E66
		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00024C6D File Offset: 0x00022E6D
		void ICollection<KeyValuePair<string, JToken>>.Add(KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00024C88 File Offset: 0x00022E88
		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00024C90 File Offset: 0x00022E90
		bool ICollection<KeyValuePair<string, JToken>>.Contains(KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key);
			return jproperty != null && jproperty.Value == item.Value;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00024CC0 File Offset: 0x00022EC0
		void ICollection<KeyValuePair<string, JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
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
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00024D7C File Offset: 0x00022F7C
		bool ICollection<KeyValuePair<string, JToken>>.Remove(KeyValuePair<string, JToken> item)
		{
			if (!this.Contains(item))
			{
				return false;
			}
			this.Remove(item.Key);
			return true;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00024D98 File Offset: 0x00022F98
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00024DA0 File Offset: 0x00022FA0
		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				yield return new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00024DAF File Offset: 0x00022FAF
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00024DC8 File Offset: 0x00022FC8
		protected virtual void OnPropertyChanging(string propertyName)
		{
			PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00024DE1 File Offset: 0x00022FE1
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties(null);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00024DEC File Offset: 0x00022FEC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (KeyValuePair<string, JToken> keyValuePair in this)
			{
				propertyDescriptorCollection.Add(new JPropertyDescriptor(keyValuePair.Key));
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00024E48 File Offset: 0x00023048
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00024290 File Offset: 0x00022490
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00024290 File Offset: 0x00022490
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00024E4F File Offset: 0x0002304F
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new TypeConverter();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00024290 File Offset: 0x00022490
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00024290 File Offset: 0x00022490
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00024290 File Offset: 0x00022490
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00024E56 File Offset: 0x00023056
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00024E56 File Offset: 0x00023056
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00024290 File Offset: 0x00022490
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return null;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00024E5D File Offset: 0x0002305D
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JObject>(parameter, this, new JObject.JObjectDynamicProxy());
		}

		// Token: 0x04000345 RID: 837
		private readonly JPropertyKeyedCollection _properties = new JPropertyKeyedCollection();

		// Token: 0x04000346 RID: 838
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanged;

		// Token: 0x04000347 RID: 839
		[CompilerGenerated]
		private PropertyChangingEventHandler PropertyChanging;

		// Token: 0x0200015A RID: 346
		private class JObjectDynamicProxy : DynamicProxy<JObject>
		{
			// Token: 0x06000D44 RID: 3396 RVA: 0x00031F74 File Offset: 0x00030174
			public override bool TryGetMember(JObject instance, GetMemberBinder binder, out object result)
			{
				result = instance[binder.Name];
				return true;
			}

			// Token: 0x06000D45 RID: 3397 RVA: 0x00031F88 File Offset: 0x00030188
			public override bool TrySetMember(JObject instance, SetMemberBinder binder, object value)
			{
				JToken jtoken = value as JToken;
				if (jtoken == null)
				{
					jtoken = new JValue(value);
				}
				instance[binder.Name] = jtoken;
				return true;
			}

			// Token: 0x06000D46 RID: 3398 RVA: 0x00031FB4 File Offset: 0x000301B4
			public override IEnumerable<string> GetDynamicMemberNames(JObject instance)
			{
				return Enumerable.Select<JProperty, string>(instance.Properties(), (JProperty p) => p.Name);
			}

			// Token: 0x06000D47 RID: 3399 RVA: 0x00031FE0 File Offset: 0x000301E0
			public JObjectDynamicProxy()
			{
			}

			// Token: 0x0200017B RID: 379
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000DF5 RID: 3573 RVA: 0x00034431 File Offset: 0x00032631
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000DF6 RID: 3574 RVA: 0x00008020 File Offset: 0x00006220
				public <>c()
				{
				}

				// Token: 0x06000DF7 RID: 3575 RVA: 0x0003443D File Offset: 0x0003263D
				internal string <GetDynamicMemberNames>b__2_0(JProperty p)
				{
					return p.Name;
				}

				// Token: 0x040005A2 RID: 1442
				public static readonly JObject.JObjectDynamicProxy.<>c <>9 = new JObject.JObjectDynamicProxy.<>c();

				// Token: 0x040005A3 RID: 1443
				public static Func<JProperty, string> <>9__2_0;
			}
		}

		// Token: 0x0200015B RID: 347
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000D48 RID: 3400 RVA: 0x00031FE8 File Offset: 0x000301E8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x00031FF4 File Offset: 0x000301F4
			internal JToken <PropertyValues>b__25_0(JProperty p)
			{
				return p.Value;
			}

			// Token: 0x040004FB RID: 1275
			public static readonly JObject.<>c <>9 = new JObject.<>c();

			// Token: 0x040004FC RID: 1276
			public static Func<JProperty, JToken> <>9__25_0;
		}

		// Token: 0x0200015C RID: 348
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__58 : IEnumerator<KeyValuePair<string, JToken>>, IDisposable, IEnumerator
		{
			// Token: 0x06000D4B RID: 3403 RVA: 0x00031FFC File Offset: 0x000301FC
			[DebuggerHidden]
			public <GetEnumerator>d__58(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000D4C RID: 3404 RVA: 0x0003200C File Offset: 0x0003020C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000D4D RID: 3405 RVA: 0x00032044 File Offset: 0x00030244
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = this._properties.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						JProperty jproperty = (JProperty)enumerator.Current;
						this.<>2__current = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
						this.<>1__state = 1;
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

			// Token: 0x06000D4E RID: 3406 RVA: 0x00032100 File Offset: 0x00030300
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000293 RID: 659
			// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003211C File Offset: 0x0003031C
			KeyValuePair<string, JToken> IEnumerator<KeyValuePair<string, JToken>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D50 RID: 3408 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000294 RID: 660
			// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00032124 File Offset: 0x00030324
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040004FD RID: 1277
			private int <>1__state;

			// Token: 0x040004FE RID: 1278
			private KeyValuePair<string, JToken> <>2__current;

			// Token: 0x040004FF RID: 1279
			public JObject <>4__this;

			// Token: 0x04000500 RID: 1280
			private IEnumerator<JToken> <>7__wrap1;
		}
	}
}
