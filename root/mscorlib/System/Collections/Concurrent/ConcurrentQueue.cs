using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AAA RID: 2730
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x060064CD RID: 25805 RVA: 0x001571A0 File Offset: 0x001553A0
		public ConcurrentQueue()
		{
			this._crossSegmentLock = new object();
			this._tail = (this._head = new ConcurrentQueue<T>.Segment(32));
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x001571D8 File Offset: 0x001553D8
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			this._crossSegmentLock = new object();
			int num = 32;
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > num)
				{
					num = Math.Min(ConcurrentQueue<T>.Segment.RoundUpToPowerOf2(count), 1048576);
				}
			}
			this._tail = (this._head = new ConcurrentQueue<T>.Segment(num));
			foreach (T t in collection)
			{
				this.Enqueue(t);
			}
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x00157274 File Offset: 0x00155474
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x00157294 File Offset: 0x00155494
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToArray().CopyTo(array, index);
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x001572CF File Offset: 0x001554CF
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001572DB File Offset: 0x001554DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x001572E3 File Offset: 0x001554E3
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x0007EA7F File Offset: 0x0007CC7F
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x001572F0 File Offset: 0x001554F0
		public bool IsEmpty
		{
			get
			{
				T t;
				return !this.TryPeek(out t, false);
			}
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x0015730C File Offset: 0x0015550C
		public T[] ToArray()
		{
			ConcurrentQueue<T>.Segment segment;
			int num;
			ConcurrentQueue<T>.Segment segment2;
			int num2;
			this.SnapForObservation(out segment, out num, out segment2, out num2);
			T[] array = new T[ConcurrentQueue<T>.GetCount(segment, num, segment2, num2)];
			using (IEnumerator<T> enumerator = this.Enumerate(segment, num, segment2, num2))
			{
				int num3 = 0;
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					array[num3++] = t;
				}
			}
			return array;
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x00157388 File Offset: 0x00155588
		public int Count
		{
			get
			{
				SpinWait spinWait = default(SpinWait);
				ConcurrentQueue<T>.Segment head;
				ConcurrentQueue<T>.Segment tail;
				int num;
				int num2;
				int num3;
				int num4;
				for (;;)
				{
					head = this._head;
					tail = this._tail;
					num = Volatile.Read(ref head._headAndTail.Head);
					num2 = Volatile.Read(ref head._headAndTail.Tail);
					if (head == tail)
					{
						if (head == this._head && head == this._tail && num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail))
						{
							break;
						}
					}
					else
					{
						if (head._nextSegment != tail)
						{
							goto IL_013C;
						}
						num3 = Volatile.Read(ref tail._headAndTail.Head);
						num4 = Volatile.Read(ref tail._headAndTail.Tail);
						if (head == this._head && tail == this._tail && num == Volatile.Read(ref head._headAndTail.Head) && num2 == Volatile.Read(ref head._headAndTail.Tail) && num3 == Volatile.Read(ref tail._headAndTail.Head) && num4 == Volatile.Read(ref tail._headAndTail.Tail))
						{
							goto Block_12;
						}
					}
					spinWait.SpinOnce();
				}
				return ConcurrentQueue<T>.GetCount(head, num, num2);
				Block_12:
				return ConcurrentQueue<T>.GetCount(head, num, num2) + ConcurrentQueue<T>.GetCount(tail, num3, num4);
				IL_013C:
				this.SnapForObservation(out head, out num, out tail, out num4);
				return (int)ConcurrentQueue<T>.GetCount(head, num, tail, num4);
			}
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x001574F6 File Offset: 0x001556F6
		private static int GetCount(ConcurrentQueue<T>.Segment s, int head, int tail)
		{
			if (head == tail || head == tail - s.FreezeOffset)
			{
				return 0;
			}
			head &= s._slotsMask;
			tail &= s._slotsMask;
			if (head >= tail)
			{
				return s._slots.Length - head + tail;
			}
			return tail - head;
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x00157534 File Offset: 0x00155734
		private static long GetCount(ConcurrentQueue<T>.Segment head, int headHead, ConcurrentQueue<T>.Segment tail, int tailTail)
		{
			long num = 0L;
			int num2 = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
			if (headHead < num2)
			{
				headHead &= head._slotsMask;
				num2 &= head._slotsMask;
				num += (long)((headHead < num2) ? (num2 - headHead) : (head._slots.Length - headHead + num2));
			}
			if (head != tail)
			{
				for (ConcurrentQueue<T>.Segment segment = head._nextSegment; segment != tail; segment = segment._nextSegment)
				{
					num += (long)(segment._headAndTail.Tail - segment.FreezeOffset);
				}
				num += (long)(tailTail - tail.FreezeOffset);
			}
			return num;
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x001575D0 File Offset: 0x001557D0
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument must be greater than or equal zero.");
			}
			ConcurrentQueue<T>.Segment segment;
			int num;
			ConcurrentQueue<T>.Segment segment2;
			int num2;
			this.SnapForObservation(out segment, out num, out segment2, out num2);
			long count = ConcurrentQueue<T>.GetCount(segment, num, segment2, num2);
			if ((long)index > (long)array.Length - count)
			{
				throw new ArgumentException("The number of elements in the collection is greater than the available space from index to the end of the destination array.");
			}
			int num3 = index;
			using (IEnumerator<T> enumerator = this.Enumerate(segment, num, segment2, num2))
			{
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					array[num3++] = t;
				}
			}
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x0015767C File Offset: 0x0015587C
		public IEnumerator<T> GetEnumerator()
		{
			ConcurrentQueue<T>.Segment segment;
			int num;
			ConcurrentQueue<T>.Segment segment2;
			int num2;
			this.SnapForObservation(out segment, out num, out segment2, out num2);
			return this.Enumerate(segment, num, segment2, num2);
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x001576A4 File Offset: 0x001558A4
		private void SnapForObservation(out ConcurrentQueue<T>.Segment head, out int headHead, out ConcurrentQueue<T>.Segment tail, out int tailTail)
		{
			object crossSegmentLock = this._crossSegmentLock;
			lock (crossSegmentLock)
			{
				head = this._head;
				tail = this._tail;
				ConcurrentQueue<T>.Segment segment = head;
				for (;;)
				{
					segment._preservedForObservation = true;
					if (segment == tail)
					{
						break;
					}
					segment = segment._nextSegment;
				}
				tail.EnsureFrozenForEnqueues();
				headHead = Volatile.Read(ref head._headAndTail.Head);
				tailTail = Volatile.Read(ref tail._headAndTail.Tail);
			}
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x00157738 File Offset: 0x00155938
		private T GetItemWhenAvailable(ConcurrentQueue<T>.Segment segment, int i)
		{
			int num = (i + 1) & segment._slotsMask;
			if ((segment._slots[i].SequenceNumber & segment._slotsMask) != num)
			{
				SpinWait spinWait = default(SpinWait);
				while ((Volatile.Read(ref segment._slots[i].SequenceNumber) & segment._slotsMask) != num)
				{
					spinWait.SpinOnce();
				}
			}
			return segment._slots[i].Item;
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001577AD File Offset: 0x001559AD
		private IEnumerator<T> Enumerate(ConcurrentQueue<T>.Segment head, int headHead, ConcurrentQueue<T>.Segment tail, int tailTail)
		{
			int headTail = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
			if (headHead < headTail)
			{
				headHead &= head._slotsMask;
				headTail &= head._slotsMask;
				if (headHead < headTail)
				{
					int num;
					for (int i = headHead; i < headTail; i = num + 1)
					{
						yield return this.GetItemWhenAvailable(head, i);
						num = i;
					}
				}
				else
				{
					int num;
					for (int i = headHead; i < head._slots.Length; i = num + 1)
					{
						yield return this.GetItemWhenAvailable(head, i);
						num = i;
					}
					for (int i = 0; i < headTail; i = num + 1)
					{
						yield return this.GetItemWhenAvailable(head, i);
						num = i;
					}
				}
			}
			if (head != tail)
			{
				int num;
				ConcurrentQueue<T>.Segment s;
				for (s = head._nextSegment; s != tail; s = s._nextSegment)
				{
					int i = s._headAndTail.Tail - s.FreezeOffset;
					for (int j = 0; j < i; j = num + 1)
					{
						yield return this.GetItemWhenAvailable(s, j);
						num = j;
					}
				}
				s = null;
				tailTail -= tail.FreezeOffset;
				for (int i = 0; i < tailTail; i = num + 1)
				{
					yield return this.GetItemWhenAvailable(tail, i);
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x001577D9 File Offset: 0x001559D9
		public void Enqueue(T item)
		{
			if (!this._tail.TryEnqueue(item))
			{
				this.EnqueueSlow(item);
			}
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x001577F4 File Offset: 0x001559F4
		private void EnqueueSlow(T item)
		{
			for (;;)
			{
				ConcurrentQueue<T>.Segment tail = this._tail;
				if (tail.TryEnqueue(item))
				{
					break;
				}
				object crossSegmentLock = this._crossSegmentLock;
				lock (crossSegmentLock)
				{
					if (tail == this._tail)
					{
						tail.EnsureFrozenForEnqueues();
						ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(tail._preservedForObservation ? 32 : Math.Min(tail.Capacity * 2, 1048576));
						tail._nextSegment = segment;
						this._tail = segment;
					}
				}
			}
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x00157894 File Offset: 0x00155A94
		public bool TryDequeue(out T result)
		{
			return this._head.TryDequeue(out result) || this.TryDequeueSlow(out result);
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x001578B0 File Offset: 0x00155AB0
		private bool TryDequeueSlow(out T item)
		{
			for (;;)
			{
				ConcurrentQueue<T>.Segment head = this._head;
				if (head.TryDequeue(out item))
				{
					break;
				}
				if (head._nextSegment == null)
				{
					goto Block_1;
				}
				if (head.TryDequeue(out item))
				{
					return true;
				}
				object crossSegmentLock = this._crossSegmentLock;
				lock (crossSegmentLock)
				{
					if (head == this._head)
					{
						this._head = head._nextSegment;
					}
				}
			}
			return true;
			Block_1:
			item = default(T);
			return false;
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x00157940 File Offset: 0x00155B40
		public bool TryPeek(out T result)
		{
			return this.TryPeek(out result, true);
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x0015794C File Offset: 0x00155B4C
		private bool TryPeek(out T result, bool resultUsed)
		{
			ConcurrentQueue<T>.Segment segment = this._head;
			for (;;)
			{
				ConcurrentQueue<T>.Segment segment2 = Volatile.Read<ConcurrentQueue<T>.Segment>(ref segment._nextSegment);
				if (segment.TryPeek(out result, resultUsed))
				{
					break;
				}
				if (segment2 != null)
				{
					segment = segment2;
				}
				else if (Volatile.Read<ConcurrentQueue<T>.Segment>(ref segment._nextSegment) == null)
				{
					goto Block_3;
				}
			}
			return true;
			Block_3:
			result = default(T);
			return false;
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x00157998 File Offset: 0x00155B98
		public void Clear()
		{
			object crossSegmentLock = this._crossSegmentLock;
			lock (crossSegmentLock)
			{
				this._tail.EnsureFrozenForEnqueues();
				this._tail = (this._head = new ConcurrentQueue<T>.Segment(32));
			}
		}

		// Token: 0x04003B49 RID: 15177
		private const int InitialSegmentLength = 32;

		// Token: 0x04003B4A RID: 15178
		private const int MaxSegmentLength = 1048576;

		// Token: 0x04003B4B RID: 15179
		private object _crossSegmentLock;

		// Token: 0x04003B4C RID: 15180
		private volatile ConcurrentQueue<T>.Segment _tail;

		// Token: 0x04003B4D RID: 15181
		private volatile ConcurrentQueue<T>.Segment _head;

		// Token: 0x02000AAB RID: 2731
		[DebuggerDisplay("Capacity = {Capacity}")]
		internal sealed class Segment
		{
			// Token: 0x060064E7 RID: 25831 RVA: 0x001579FC File Offset: 0x00155BFC
			public Segment(int boundedLength)
			{
				this._slots = new ConcurrentQueue<T>.Segment.Slot[boundedLength];
				this._slotsMask = boundedLength - 1;
				for (int i = 0; i < this._slots.Length; i++)
				{
					this._slots[i].SequenceNumber = i;
				}
			}

			// Token: 0x060064E8 RID: 25832 RVA: 0x00157A49 File Offset: 0x00155C49
			internal static int RoundUpToPowerOf2(int i)
			{
				i--;
				i |= i >> 1;
				i |= i >> 2;
				i |= i >> 4;
				i |= i >> 8;
				i |= i >> 16;
				return i + 1;
			}

			// Token: 0x1700116E RID: 4462
			// (get) Token: 0x060064E9 RID: 25833 RVA: 0x00157A77 File Offset: 0x00155C77
			internal int Capacity
			{
				get
				{
					return this._slots.Length;
				}
			}

			// Token: 0x1700116F RID: 4463
			// (get) Token: 0x060064EA RID: 25834 RVA: 0x00157A81 File Offset: 0x00155C81
			internal int FreezeOffset
			{
				get
				{
					return this._slots.Length * 2;
				}
			}

			// Token: 0x060064EB RID: 25835 RVA: 0x00157A90 File Offset: 0x00155C90
			internal void EnsureFrozenForEnqueues()
			{
				if (!this._frozenForEnqueues)
				{
					this._frozenForEnqueues = true;
					SpinWait spinWait = default(SpinWait);
					for (;;)
					{
						int num = Volatile.Read(ref this._headAndTail.Tail);
						if (Interlocked.CompareExchange(ref this._headAndTail.Tail, num + this.FreezeOffset, num) == num)
						{
							break;
						}
						spinWait.SpinOnce();
					}
				}
			}

			// Token: 0x060064EC RID: 25836 RVA: 0x00157AEC File Offset: 0x00155CEC
			public bool TryDequeue(out T item)
			{
				SpinWait spinWait = default(SpinWait);
				int num;
				int num2;
				for (;;)
				{
					num = Volatile.Read(ref this._headAndTail.Head);
					num2 = num & this._slotsMask;
					int num3 = Volatile.Read(ref this._slots[num2].SequenceNumber) - (num + 1);
					if (num3 == 0)
					{
						if (Interlocked.CompareExchange(ref this._headAndTail.Head, num + 1, num) == num)
						{
							break;
						}
					}
					else if (num3 < 0)
					{
						bool frozenForEnqueues = this._frozenForEnqueues;
						int num4 = Volatile.Read(ref this._headAndTail.Tail);
						if (num4 - num <= 0 || (frozenForEnqueues && num4 - this.FreezeOffset - num <= 0))
						{
							goto IL_00EE;
						}
					}
					spinWait.SpinOnce();
				}
				item = this._slots[num2].Item;
				if (!Volatile.Read(ref this._preservedForObservation))
				{
					this._slots[num2].Item = default(T);
					Volatile.Write(ref this._slots[num2].SequenceNumber, num + this._slots.Length);
				}
				return true;
				IL_00EE:
				item = default(T);
				return false;
			}

			// Token: 0x060064ED RID: 25837 RVA: 0x00157BFC File Offset: 0x00155DFC
			public bool TryPeek(out T result, bool resultUsed)
			{
				if (resultUsed)
				{
					this._preservedForObservation = true;
					Interlocked.MemoryBarrier();
				}
				SpinWait spinWait = default(SpinWait);
				int num2;
				for (;;)
				{
					int num = Volatile.Read(ref this._headAndTail.Head);
					num2 = num & this._slotsMask;
					int num3 = Volatile.Read(ref this._slots[num2].SequenceNumber) - (num + 1);
					if (num3 == 0)
					{
						break;
					}
					if (num3 < 0)
					{
						bool frozenForEnqueues = this._frozenForEnqueues;
						int num4 = Volatile.Read(ref this._headAndTail.Tail);
						if (num4 - num <= 0 || (frozenForEnqueues && num4 - this.FreezeOffset - num <= 0))
						{
							goto IL_00AE;
						}
					}
					spinWait.SpinOnce();
				}
				result = (resultUsed ? this._slots[num2].Item : default(T));
				return true;
				IL_00AE:
				result = default(T);
				return false;
			}

			// Token: 0x060064EE RID: 25838 RVA: 0x00157CCC File Offset: 0x00155ECC
			public bool TryEnqueue(T item)
			{
				SpinWait spinWait = default(SpinWait);
				int num;
				int num2;
				for (;;)
				{
					num = Volatile.Read(ref this._headAndTail.Tail);
					num2 = num & this._slotsMask;
					int num3 = Volatile.Read(ref this._slots[num2].SequenceNumber) - num;
					if (num3 == 0)
					{
						if (Interlocked.CompareExchange(ref this._headAndTail.Tail, num + 1, num) == num)
						{
							break;
						}
					}
					else if (num3 < 0)
					{
						return false;
					}
					spinWait.SpinOnce();
				}
				this._slots[num2].Item = item;
				Volatile.Write(ref this._slots[num2].SequenceNumber, num + 1);
				return true;
			}

			// Token: 0x04003B4E RID: 15182
			internal readonly ConcurrentQueue<T>.Segment.Slot[] _slots;

			// Token: 0x04003B4F RID: 15183
			internal readonly int _slotsMask;

			// Token: 0x04003B50 RID: 15184
			internal PaddedHeadAndTail _headAndTail;

			// Token: 0x04003B51 RID: 15185
			internal bool _preservedForObservation;

			// Token: 0x04003B52 RID: 15186
			internal bool _frozenForEnqueues;

			// Token: 0x04003B53 RID: 15187
			internal ConcurrentQueue<T>.Segment _nextSegment;

			// Token: 0x02000AAC RID: 2732
			[DebuggerDisplay("Item = {Item}, SequenceNumber = {SequenceNumber}")]
			[StructLayout(LayoutKind.Auto)]
			internal struct Slot
			{
				// Token: 0x04003B54 RID: 15188
				public T Item;

				// Token: 0x04003B55 RID: 15189
				public int SequenceNumber;
			}
		}

		// Token: 0x02000AAD RID: 2733
		[CompilerGenerated]
		private sealed class <Enumerate>d__28 : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060064EF RID: 25839 RVA: 0x00157D6C File Offset: 0x00155F6C
			[DebuggerHidden]
			public <Enumerate>d__28(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060064F0 RID: 25840 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060064F1 RID: 25841 RVA: 0x00157D7C File Offset: 0x00155F7C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ConcurrentQueue<T> concurrentQueue = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					headTail = ((head == tail) ? tailTail : Volatile.Read(ref head._headAndTail.Tail)) - head.FreezeOffset;
					if (headHead >= headTail)
					{
						goto IL_01C4;
					}
					headHead &= head._slotsMask;
					headTail &= head._slotsMask;
					if (headHead >= headTail)
					{
						i = headHead;
						goto IL_0160;
					}
					i = headHead;
					break;
				case 1:
				{
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
					break;
				}
				case 2:
				{
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
					goto IL_0160;
				}
				case 3:
				{
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
					goto IL_01B6;
				}
				case 4:
				{
					this.<>1__state = -1;
					int num2 = j;
					j = num2 + 1;
					goto IL_024E;
				}
				case 5:
				{
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
					goto IL_02DE;
				}
				default:
					return false;
				}
				if (i >= headTail)
				{
					goto IL_01C4;
				}
				this.<>2__current = concurrentQueue.GetItemWhenAvailable(head, i);
				this.<>1__state = 1;
				return true;
				IL_0160:
				if (i < head._slots.Length)
				{
					this.<>2__current = concurrentQueue.GetItemWhenAvailable(head, i);
					this.<>1__state = 2;
					return true;
				}
				i = 0;
				IL_01B6:
				if (i < headTail)
				{
					this.<>2__current = concurrentQueue.GetItemWhenAvailable(head, i);
					this.<>1__state = 3;
					return true;
				}
				IL_01C4:
				if (head != tail)
				{
					s = head._nextSegment;
					goto IL_026D;
				}
				return false;
				IL_024E:
				if (j < i)
				{
					this.<>2__current = concurrentQueue.GetItemWhenAvailable(s, j);
					this.<>1__state = 4;
					return true;
				}
				s = s._nextSegment;
				IL_026D:
				if (s != tail)
				{
					i = s._headAndTail.Tail - s.FreezeOffset;
					j = 0;
					goto IL_024E;
				}
				s = null;
				tailTail -= tail.FreezeOffset;
				i = 0;
				IL_02DE:
				if (i < tailTail)
				{
					this.<>2__current = concurrentQueue.GetItemWhenAvailable(tail, i);
					this.<>1__state = 5;
					return true;
				}
				return false;
			}

			// Token: 0x17001170 RID: 4464
			// (get) Token: 0x060064F2 RID: 25842 RVA: 0x00158076 File Offset: 0x00156276
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060064F3 RID: 25843 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001171 RID: 4465
			// (get) Token: 0x060064F4 RID: 25844 RVA: 0x0015807E File Offset: 0x0015627E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04003B56 RID: 15190
			private int <>1__state;

			// Token: 0x04003B57 RID: 15191
			private T <>2__current;

			// Token: 0x04003B58 RID: 15192
			public ConcurrentQueue<T>.Segment head;

			// Token: 0x04003B59 RID: 15193
			public ConcurrentQueue<T>.Segment tail;

			// Token: 0x04003B5A RID: 15194
			public int tailTail;

			// Token: 0x04003B5B RID: 15195
			public int headHead;

			// Token: 0x04003B5C RID: 15196
			public ConcurrentQueue<T> <>4__this;

			// Token: 0x04003B5D RID: 15197
			private int <headTail>5__2;

			// Token: 0x04003B5E RID: 15198
			private int <i>5__3;

			// Token: 0x04003B5F RID: 15199
			private ConcurrentQueue<T>.Segment <s>5__4;

			// Token: 0x04003B60 RID: 15200
			private int <i>5__5;
		}
	}
}
