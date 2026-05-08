using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x0200061E RID: 1566
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000D0F1F File Offset: 0x000CF11F
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this._members = members;
			this._data = info;
			this._types = types;
			this._numItems = numItems - 1;
			this._currItem = -1;
			this._current = false;
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000D0F54 File Offset: 0x000CF154
		public bool MoveNext()
		{
			if (this._currItem < this._numItems)
			{
				this._currItem++;
				this._current = true;
			}
			else
			{
				this._current = false;
			}
			return this._current;
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000D0F88 File Offset: 0x000CF188
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x000D0F98 File Offset: 0x000CF198
		public SerializationEntry Current
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return new SerializationEntry(this._members[this._currItem], this._data[this._currItem], this._types[this._currItem]);
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000D0FE4 File Offset: 0x000CF1E4
		public void Reset()
		{
			this._currItem = -1;
			this._current = false;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x000D0FF4 File Offset: 0x000CF1F4
		public string Name
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._members[this._currItem];
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000D1016 File Offset: 0x000CF216
		public object Value
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._data[this._currItem];
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000D1038 File Offset: 0x000CF238
		public Type ObjectType
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._types[this._currItem];
			}
		}

		// Token: 0x0400269D RID: 9885
		private readonly string[] _members;

		// Token: 0x0400269E RID: 9886
		private readonly object[] _data;

		// Token: 0x0400269F RID: 9887
		private readonly Type[] _types;

		// Token: 0x040026A0 RID: 9888
		private readonly int _numItems;

		// Token: 0x040026A1 RID: 9889
		private int _currItem;

		// Token: 0x040026A2 RID: 9890
		private bool _current;
	}
}
