using System;

namespace System.Threading
{
	// Token: 0x02000286 RID: 646
	internal class SparselyPopulatedArrayFragment<T> where T : class
	{
		// Token: 0x06001E20 RID: 7712 RVA: 0x00070E5A File Offset: 0x0006F05A
		internal SparselyPopulatedArrayFragment(int size)
			: this(size, null)
		{
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00070E64 File Offset: 0x0006F064
		internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
		{
			this._elements = new T[size];
			this._freeCount = size;
			this._prev = prev;
		}

		// Token: 0x17000391 RID: 913
		internal T this[int index]
		{
			get
			{
				return Volatile.Read<T>(ref this._elements[index]);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00070E9D File Offset: 0x0006F09D
		internal int Length
		{
			get
			{
				return this._elements.Length;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00070EA7 File Offset: 0x0006F0A7
		internal SparselyPopulatedArrayFragment<T> Prev
		{
			get
			{
				return this._prev;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00070EB4 File Offset: 0x0006F0B4
		internal T SafeAtomicRemove(int index, T expectedElement)
		{
			T t = Interlocked.CompareExchange<T>(ref this._elements[index], default(T), expectedElement);
			if (t != null)
			{
				this._freeCount++;
			}
			return t;
		}

		// Token: 0x0400197E RID: 6526
		internal readonly T[] _elements;

		// Token: 0x0400197F RID: 6527
		internal volatile int _freeCount;

		// Token: 0x04001980 RID: 6528
		internal volatile SparselyPopulatedArrayFragment<T> _next;

		// Token: 0x04001981 RID: 6529
		internal volatile SparselyPopulatedArrayFragment<T> _prev;
	}
}
