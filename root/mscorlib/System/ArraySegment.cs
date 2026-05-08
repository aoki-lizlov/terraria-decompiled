using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	public readonly struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00018BFA File Offset: 0x00016DFA
		public static ArraySegment<T> Empty
		{
			[CompilerGenerated]
			get
			{
				return ArraySegment<T>.<Empty>k__BackingField;
			}
		} = new ArraySegment<T>(new T[0]);

		// Token: 0x0600054D RID: 1357 RVA: 0x00018C01 File Offset: 0x00016E01
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018C23 File Offset: 0x00016E23
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null || offset > array.Length || count > array.Length - offset)
			{
				ThrowHelper.ThrowArraySegmentCtorValidationFailedExceptions(array, offset, count);
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00018C53 File Offset: 0x00016E53
		public T[] Array
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00018C5B File Offset: 0x00016E5B
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00018C63 File Offset: 0x00016E63
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700008C RID: 140
		public T this[int index]
		{
			get
			{
				if (index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
			set
			{
				if (index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00018CB2 File Offset: 0x00016EB2
		public ArraySegment<T>.Enumerator GetEnumerator()
		{
			this.ThrowInvalidOperationIfDefault();
			return new ArraySegment<T>.Enumerator(this);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018CC5 File Offset: 0x00016EC5
		public override int GetHashCode()
		{
			if (this._array == null)
			{
				return 0;
			}
			return global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.Combine(5381, this._offset), this._count) ^ this._array.GetHashCode();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00018CF8 File Offset: 0x00016EF8
		public void CopyTo(T[] destination)
		{
			this.CopyTo(destination, 0);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00018D02 File Offset: 0x00016F02
		public void CopyTo(T[] destination, int destinationIndex)
		{
			this.ThrowInvalidOperationIfDefault();
			global::System.Array.Copy(this._array, this._offset, destination, destinationIndex, this._count);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00018D24 File Offset: 0x00016F24
		public void CopyTo(ArraySegment<T> destination)
		{
			this.ThrowInvalidOperationIfDefault();
			destination.ThrowInvalidOperationIfDefault();
			if (this._count > destination._count)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			global::System.Array.Copy(this._array, this._offset, destination._array, destination._offset, this._count);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00018D74 File Offset: 0x00016F74
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00018D8C File Offset: 0x00016F8C
		public bool Equals(ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00018DBA File Offset: 0x00016FBA
		public ArraySegment<T> Slice(int index)
		{
			this.ThrowInvalidOperationIfDefault();
			if (index > this._count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return new ArraySegment<T>(this._array, this._offset + index, this._count - index);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00018DEB File Offset: 0x00016FEB
		public ArraySegment<T> Slice(int index, int count)
		{
			this.ThrowInvalidOperationIfDefault();
			if (index > this._count || count > this._count - index)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return new ArraySegment<T>(this._array, this._offset + index, count);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00018E20 File Offset: 0x00017020
		public T[] ToArray()
		{
			this.ThrowInvalidOperationIfDefault();
			if (this._count == 0)
			{
				return ArraySegment<T>.Empty._array;
			}
			T[] array = new T[this._count];
			global::System.Array.Copy(this._array, this._offset, array, 0, this._count);
			return array;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00018E6C File Offset: 0x0001706C
		public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00018E76 File Offset: 0x00017076
		public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00018E84 File Offset: 0x00017084
		public static implicit operator ArraySegment<T>(T[] array)
		{
			if (array == null)
			{
				return default(ArraySegment<T>);
			}
			return new ArraySegment<T>(array);
		}

		// Token: 0x1700008D RID: 141
		T IList<T>.this[int index]
		{
			get
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
			set
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00018F00 File Offset: 0x00017100
		int IList<T>.IndexOf(T item)
		{
			this.ThrowInvalidOperationIfDefault();
			int num = global::System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00018F3A File Offset: 0x0001713A
		void IList<T>.Insert(int index, T item)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00018F3A File Offset: 0x0001713A
		void IList<T>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x1700008E RID: 142
		T IReadOnlyList<T>.this[int index]
		{
			get
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00018F3A File Offset: 0x0001713A
		void ICollection<T>.Add(T item)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00018F3A File Offset: 0x0001713A
		void ICollection<T>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018F41 File Offset: 0x00017141
		bool ICollection<T>.Contains(T item)
		{
			this.ThrowInvalidOperationIfDefault();
			return global::System.Array.IndexOf<T>(this._array, item, this._offset, this._count) >= 0;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018F67 File Offset: 0x00017167
		bool ICollection<T>.Remove(T item)
		{
			ThrowHelper.ThrowNotSupportedException();
			return false;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018F6F File Offset: 0x0001716F
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00018F6F File Offset: 0x0001716F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00018F7C File Offset: 0x0001717C
		private void ThrowInvalidOperationIfDefault()
		{
			if (this._array == null)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NullArray);
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018F8D File Offset: 0x0001718D
		// Note: this type is marked as 'beforefieldinit'.
		static ArraySegment()
		{
		}

		// Token: 0x04000ECD RID: 3789
		[CompilerGenerated]
		private static readonly ArraySegment<T> <Empty>k__BackingField;

		// Token: 0x04000ECE RID: 3790
		private readonly T[] _array;

		// Token: 0x04000ECF RID: 3791
		private readonly int _offset;

		// Token: 0x04000ED0 RID: 3792
		private readonly int _count;

		// Token: 0x020000BF RID: 191
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000570 RID: 1392 RVA: 0x00018F9F File Offset: 0x0001719F
			internal Enumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment.Array;
				this._start = arraySegment.Offset;
				this._end = arraySegment.Offset + arraySegment.Count;
				this._current = arraySegment.Offset - 1;
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x00018FDF File Offset: 0x000171DF
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001900D File Offset: 0x0001720D
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumNotStarted();
					}
					if (this._current >= this._end)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumEnded();
					}
					return this._array[this._current];
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x00019046 File Offset: 0x00017246
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00019053 File Offset: 0x00017253
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x04000ED1 RID: 3793
			private readonly T[] _array;

			// Token: 0x04000ED2 RID: 3794
			private readonly int _start;

			// Token: 0x04000ED3 RID: 3795
			private readonly int _end;

			// Token: 0x04000ED4 RID: 3796
			private int _current;
		}
	}
}
