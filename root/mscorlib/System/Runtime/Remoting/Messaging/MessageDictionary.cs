using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005FA RID: 1530
	[Serializable]
	internal class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003AF4 RID: 15092 RVA: 0x000CEA6E File Offset: 0x000CCC6E
		public MessageDictionary(IMethodMessage message)
		{
			this._message = message;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000CEA7D File Offset: 0x000CCC7D
		internal bool HasUserData()
		{
			if (this._internalProperties == null)
			{
				return false;
			}
			if (this._internalProperties is MessageDictionary)
			{
				return ((MessageDictionary)this._internalProperties).HasUserData();
			}
			return this._internalProperties.Count > 0;
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000CEAB5 File Offset: 0x000CCCB5
		internal IDictionary InternalDictionary
		{
			get
			{
				if (this._internalProperties != null && this._internalProperties is MessageDictionary)
				{
					return ((MessageDictionary)this._internalProperties).InternalDictionary;
				}
				return this._internalProperties;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000CEAE3 File Offset: 0x000CCCE3
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x000CEAEB File Offset: 0x000CCCEB
		public string[] MethodKeys
		{
			get
			{
				return this._methodKeys;
			}
			set
			{
				this._methodKeys = value;
			}
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x000CEAF4 File Offset: 0x000CCCF4
		protected virtual IDictionary AllocInternalProperties()
		{
			this._ownProperties = true;
			return new Hashtable();
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000CEB02 File Offset: 0x000CCD02
		public IDictionary GetInternalProperties()
		{
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			return this._internalProperties;
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000CEB20 File Offset: 0x000CCD20
		private bool IsOverridenKey(string key)
		{
			if (this._ownProperties)
			{
				return false;
			}
			foreach (string text in this._methodKeys)
			{
				if (key == text)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000CEB5C File Offset: 0x000CCD5C
		public MessageDictionary(string[] keys)
		{
			this._methodKeys = keys;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008DC RID: 2268
		public object this[object key]
		{
			get
			{
				string text = (string)key;
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					if (this._methodKeys[i] == text)
					{
						return this.GetMethodProperty(text);
					}
				}
				if (this._internalProperties != null)
				{
					return this._internalProperties[key];
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000CEBCC File Offset: 0x000CCDCC
		protected virtual object GetMethodProperty(string key)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								return this._message.LogicalCallContext;
							}
						}
					}
					else if (key == "__Uri")
					{
						return this._message.Uri;
					}
				}
				else if (num != 1619225942U)
				{
					if (num == 1637783905U)
					{
						if (key == "__Return")
						{
							return ((IMethodReturnMessage)this._message).ReturnValue;
						}
					}
				}
				else if (key == "__Args")
				{
					return this._message.Args;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num != 1960967436U)
				{
					if (num == 2010141056U)
					{
						if (key == "__TypeName")
						{
							return this._message.TypeName;
						}
					}
				}
				else if (key == "__OutArgs")
				{
					return ((IMethodReturnMessage)this._message).OutArgs;
				}
			}
			else if (num != 3166241401U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						return this._message.MethodSignature;
					}
				}
			}
			else if (key == "__MethodName")
			{
				return this._message.MethodName;
			}
			return null;
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000CED50 File Offset: 0x000CCF50
		protected virtual void SetMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num != 1201911322U)
						{
							return;
						}
						key == "__CallContext";
						return;
					}
					else
					{
						if (!(key == "__Uri"))
						{
							return;
						}
						((IInternalMessage)this._message).Uri = (string)value;
						return;
					}
				}
				else
				{
					if (num == 1619225942U)
					{
						key == "__Args";
						return;
					}
					if (num != 1637783905U)
					{
						return;
					}
					key == "__Return";
					return;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num == 1960967436U)
				{
					key == "__OutArgs";
					return;
				}
				if (num != 2010141056U)
				{
					return;
				}
				key == "__TypeName";
				return;
			}
			else
			{
				if (num == 3166241401U)
				{
					key == "__MethodName";
					return;
				}
				if (num != 3679129400U)
				{
					return;
				}
				key == "__MethodSignature";
				return;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000CEE48 File Offset: 0x000CD048
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this._methodKeys[i]);
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties.Keys)
					{
						string text = (string)obj;
						if (!this.IsOverridenKey(text))
						{
							arrayList.Add(text);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000CEEE4 File Offset: 0x000CD0E4
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this.GetMethodProperty(this._methodKeys[i]));
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!this.IsOverridenKey((string)dictionaryEntry.Key))
						{
							arrayList.Add(dictionaryEntry.Value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000CEF94 File Offset: 0x000CD194
		public void Add(object key, object value)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					this.SetMethodProperty(text, value);
					return;
				}
			}
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			this._internalProperties[key] = value;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000CEFF5 File Offset: 0x000CD1F5
		public void Clear()
		{
			if (this._internalProperties != null)
			{
				this._internalProperties.Clear();
			}
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000CF00C File Offset: 0x000CD20C
		public bool Contains(object key)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					return true;
				}
			}
			return this._internalProperties != null && this._internalProperties.Contains(key);
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000CF05C File Offset: 0x000CD25C
		public void Remove(object key)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					throw new ArgumentException("key was invalid");
				}
			}
			if (this._internalProperties != null)
			{
				this._internalProperties.Remove(key);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000CF0B2 File Offset: 0x000CD2B2
		public int Count
		{
			get
			{
				if (this._internalProperties != null)
				{
					return this._internalProperties.Count + this._methodKeys.Length;
				}
				return this._methodKeys.Length;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000CF0D9 File Offset: 0x000CD2D9
		public void CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x000CF0E8 File Offset: 0x000CD2E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x000CF0E8 File Offset: 0x000CD2E8
		public IDictionaryEnumerator GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x04002629 RID: 9769
		private IDictionary _internalProperties;

		// Token: 0x0400262A RID: 9770
		protected IMethodMessage _message;

		// Token: 0x0400262B RID: 9771
		private string[] _methodKeys;

		// Token: 0x0400262C RID: 9772
		private bool _ownProperties;

		// Token: 0x020005FB RID: 1531
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003B0F RID: 15119 RVA: 0x000CF0F0 File Offset: 0x000CD2F0
			public DictionaryEnumerator(MessageDictionary methodDictionary)
			{
				this._methodDictionary = methodDictionary;
				this._hashtableEnum = ((this._methodDictionary._internalProperties != null) ? this._methodDictionary._internalProperties.GetEnumerator() : null);
				this._posMethod = -1;
			}

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000CF12C File Offset: 0x000CD32C
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06003B11 RID: 15121 RVA: 0x000CF13C File Offset: 0x000CD33C
			public bool MoveNext()
			{
				if (this._posMethod != -2)
				{
					this._posMethod++;
					if (this._posMethod < this._methodDictionary._methodKeys.Length)
					{
						return true;
					}
					this._posMethod = -2;
				}
				if (this._hashtableEnum == null)
				{
					return false;
				}
				while (this._hashtableEnum.MoveNext())
				{
					if (!this._methodDictionary.IsOverridenKey((string)this._hashtableEnum.Key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003B12 RID: 15122 RVA: 0x000CF1B7 File Offset: 0x000CD3B7
			public void Reset()
			{
				this._posMethod = -1;
				this._hashtableEnum.Reset();
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000CF1CC File Offset: 0x000CD3CC
			public DictionaryEntry Entry
			{
				get
				{
					if (this._posMethod >= 0)
					{
						return new DictionaryEntry(this._methodDictionary._methodKeys[this._posMethod], this._methodDictionary.GetMethodProperty(this._methodDictionary._methodKeys[this._posMethod]));
					}
					if (this._posMethod == -1 || this._hashtableEnum == null)
					{
						throw new InvalidOperationException("The enumerator is positioned before the first element of the collection or after the last element");
					}
					return this._hashtableEnum.Entry;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000CF240 File Offset: 0x000CD440
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000CF25C File Offset: 0x000CD45C
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x0400262D RID: 9773
			private MessageDictionary _methodDictionary;

			// Token: 0x0400262E RID: 9774
			private IDictionaryEnumerator _hashtableEnum;

			// Token: 0x0400262F RID: 9775
			private int _posMethod;
		}
	}
}
