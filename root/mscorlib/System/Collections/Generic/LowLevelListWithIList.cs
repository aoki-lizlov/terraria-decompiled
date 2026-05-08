using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B11 RID: 2833
	internal sealed class LowLevelListWithIList<T> : LowLevelList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600683B RID: 26683 RVA: 0x001617CF File Offset: 0x0015F9CF
		public LowLevelListWithIList()
		{
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x001617D7 File Offset: 0x0015F9D7
		public LowLevelListWithIList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x001617E0 File Offset: 0x0015F9E0
		public LowLevelListWithIList(IEnumerable<T> collection)
			: base(collection)
		{
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x0600683E RID: 26686 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x001617E9 File Offset: 0x0015F9E9
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new LowLevelListWithIList<T>.Enumerator(this);
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x001617E9 File Offset: 0x0015F9E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new LowLevelListWithIList<T>.Enumerator(this);
		}

		// Token: 0x02000B12 RID: 2834
		private struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006841 RID: 26689 RVA: 0x001617F6 File Offset: 0x0015F9F6
			internal Enumerator(LowLevelListWithIList<T> list)
			{
				this._list = list;
				this._index = 0;
				this._version = list._version;
				this._current = default(T);
			}

			// Token: 0x06006842 RID: 26690 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x06006843 RID: 26691 RVA: 0x00161820 File Offset: 0x0015FA20
			public bool MoveNext()
			{
				LowLevelListWithIList<T> list = this._list;
				if (this._version == list._version && this._index < list._size)
				{
					this._current = list._items[this._index];
					this._index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06006844 RID: 26692 RVA: 0x0016187D File Offset: 0x0015FA7D
			private bool MoveNextRare()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException();
				}
				this._index = this._list._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x17001241 RID: 4673
			// (get) Token: 0x06006845 RID: 26693 RVA: 0x001618B8 File Offset: 0x0015FAB8
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17001242 RID: 4674
			// (get) Token: 0x06006846 RID: 26694 RVA: 0x001618C0 File Offset: 0x0015FAC0
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._list._size + 1)
					{
						throw new InvalidOperationException();
					}
					return this.Current;
				}
			}

			// Token: 0x06006847 RID: 26695 RVA: 0x001618F0 File Offset: 0x0015FAF0
			void IEnumerator.Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04003C5A RID: 15450
			private LowLevelListWithIList<T> _list;

			// Token: 0x04003C5B RID: 15451
			private int _index;

			// Token: 0x04003C5C RID: 15452
			private int _version;

			// Token: 0x04003C5D RID: 15453
			private T _current;
		}
	}
}
